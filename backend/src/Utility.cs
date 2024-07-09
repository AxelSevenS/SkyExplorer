using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SkyExplorer;

public static class Utility {
	public static IQueryable<TSource> InTimeFrame<TSource>(this IQueryable<TSource> source,
		Expression<Func<TSource, DateTime>> time,
		TimeFrame timeFrame,
		int offset = 0,
		bool utc = true) where TSource : class
	{
		DateTime now = utc ? DateTime.UtcNow : DateTime.Now;

		switch (timeFrame)
		{
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

	private static Expression<Func<TSource, bool>> BuildBetweenExpression<TSource>(
		Expression<Func<TSource, DateTime>> time,
		DateTime start,
		DateTime end)
	{
		ParameterExpression parameter = time.Parameters[0];
		Expression property = time.Body;

		ConstantExpression startConstant = Expression.Constant(start, typeof(DateTime));
		ConstantExpression endConstant = Expression.Constant(end, typeof(DateTime));

		BinaryExpression greaterThanOrEqual = Expression.GreaterThanOrEqual(property, startConstant);
		BinaryExpression lessThan = Expression.LessThan(property, endConstant);

		BinaryExpression andExpression = Expression.AndAlso(greaterThanOrEqual, lessThan);

		return Expression.Lambda<Func<TSource, bool>>(andExpression, parameter);
	}

	private static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
	{
		int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
		return dt.AddDays(- diff).Date;
	}

	// public static (DateOnly monday, DateOnly sunday) GetWeekSpan(int offset = 0, bool utc = true) {
	// 	DateOnly now = DateOnly.FromDateTime(utc ? DateTime.UtcNow : DateTime.Now).AddDays(offset * 7);

	// 	int todayWeekDay = ((int)now.DayOfWeek + 6) % 7; // Get today's weekday in Monday-first time

	// 	DateOnly monday = now.AddDays(-todayWeekDay);
	// 	DateOnly sunday = monday.AddDays(6);

	// 	return (monday, sunday);
	// }
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