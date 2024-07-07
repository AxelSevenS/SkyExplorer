namespace SkyExplorer;

public static class Utility {
	public static (DateOnly monday, DateOnly sunday) GetWeekSpan(int offset = 0, bool utc = true) {
		DateOnly now = DateOnly.FromDateTime(utc ? DateTime.UtcNow : DateTime.Now).AddDays(offset * 7);

		DateOnly monday = now.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
		if (now.DayOfWeek == DayOfWeek.Sunday) {
			monday = monday.AddDays(-7);
		}
		DateOnly sunday = now.AddDays(-(int)now.DayOfWeek);

		return (monday, sunday);
	}
}