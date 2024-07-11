using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SkyExplorer;

public static class Utility {
	public static IQueryable<TSource> InTimeFrame<TSource>(this IQueryable<TSource> source,
		Expression<Func<TSource, DateTime>> time,
		TimeFrame timeFrame,
		int offset = 0,
		bool utc = true) where TSource : class {
		DateTime now = utc ? DateTime.UtcNow : DateTime.Now;

		switch (timeFrame) {
			case TimeFrame.Daily:
				DateTime today = now.Date.AddDays(offset);
				DateTime tomorrow = today.AddDays(1);
				return source.Where(BuildBetweenExpression(time, today, tomorrow));

			case TimeFrame.Weekly:
				DateTime startOfWeek = StartOfWeek(now, DayOfWeek.Monday).AddDays(offset * 7);
				DateTime endOfWeek = startOfWeek.AddDays(7);
				return source.Where(BuildBetweenExpression(time, startOfWeek, endOfWeek));

			case TimeFrame.Monthly:
				DateTime startOfMonth = new DateTime(now.Year, now.Month, 1).AddMonths(offset);
				DateTime endOfMonth = startOfMonth.AddMonths(1);
				return source.Where(BuildBetweenExpression(time, startOfMonth, endOfMonth));

			case TimeFrame.Yearly:
				DateTime startOfYear = new DateTime(now.Year, 1, 1).AddYears(offset);
				DateTime endOfYear = startOfYear.AddYears(1);
				return source.Where(BuildBetweenExpression(time, startOfYear, endOfYear));

			case TimeFrame.AllTime:
			default:
				return source;
		}
	}

	public static IQueryable<TSource> InDateFrame<TSource>(
		this IQueryable<TSource> source,
		Expression<Func<TSource, DateTime>> time,
		DateFrame dateFrame,
		int offset = 0,
		bool utc = true) where TSource : class {
		DateTime now = utc ? DateTime.UtcNow : DateTime.Now;

		switch (dateFrame) {
			case DateFrame.Past:
				return source.Where(BuildPastExpression(time, now, offset));

			case DateFrame.Future:
				return source.Where(BuildFutureExpression(time, now, offset));

			case DateFrame.Today:
				DateTime todayStart = now.Date;
				DateTime tomorrowStart = todayStart.AddDays(1);
				return source.Where(BuildBetweenExpression(time, todayStart, tomorrowStart));

			case DateFrame.AllTime:
			default:
				return source;
		}
	}

	private static Expression<Func<TSource, bool>> BuildPastExpression<TSource>(
		Expression<Func<TSource, DateTime>> time,
		DateTime now,
		int offset) {
		var parameter = time.Parameters[0];
		var property = time.Body;

		var pastConstant = Expression.Constant(now.AddDays(offset), typeof(DateTime));
		var lessThan = Expression.LessThan(property, pastConstant);

		return Expression.Lambda<Func<TSource, bool>>(lessThan, parameter);
	}

	private static Expression<Func<TSource, bool>> BuildFutureExpression<TSource>(
		Expression<Func<TSource, DateTime>> time,
		DateTime now,
		int offset) {
		var parameter = time.Parameters[0];
		var property = time.Body;

		var futureConstant = Expression.Constant(now.AddDays(offset), typeof(DateTime));
		var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, futureConstant);

		return Expression.Lambda<Func<TSource, bool>>(greaterThanOrEqual, parameter);
	}

	private static Expression<Func<TSource, bool>> BuildBetweenExpression<TSource>(
		Expression<Func<TSource, DateTime>> time,
		DateTime start,
		DateTime end) {
		ParameterExpression parameter = time.Parameters[0];
		Expression property = time.Body;

		ConstantExpression startConstant = Expression.Constant(start, typeof(DateTime));
		ConstantExpression endConstant = Expression.Constant(end, typeof(DateTime));

		BinaryExpression greaterThanOrEqual = Expression.GreaterThanOrEqual(property, startConstant);
		BinaryExpression lessThan = Expression.LessThan(property, endConstant);

		BinaryExpression andExpression = Expression.AndAlso(greaterThanOrEqual, lessThan);

		return Expression.Lambda<Func<TSource, bool>>(andExpression, parameter);
	}

	private static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek) {
		int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
		return dt.AddDays(-diff).Date;
	}
}

[Serializable]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DateFrame {
	AllTime,
	Past,
	Future,
	Today,
}

[Serializable]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TimeFrame {
	AllTime,
	Yearly,
	Monthly,
	Weekly,
	Daily
}

[Serializable]
public record class CourseQuerySettings {
	[JsonPropertyName("timeFrame")]
	public TimeFrame TimeFrame = TimeFrame.AllTime;

	[JsonPropertyName("dateFrame")]
	public DateFrame DateFrame = DateFrame.AllTime;

	[JsonPropertyName("offset")]
	public int Offset = 0;

	public CourseQuerySettings() { }
}