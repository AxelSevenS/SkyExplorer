namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/flights")]
public class FlightController(AppDbContext repo) : Controller<Flight, FlightCreateDTO, FlightUpdateDTO>(repo) {
	protected override DbSet<Flight> Set => Repository.Flights;


	[HttpGet("hours")]
	public async Task<ActionResult<TimeSpan>> GetHoursSum() {
		return Ok(await Repository.Flights.SumAsync(f => f.Duration.TotalHours));
	}

	[HttpGet("hours/{id}")]
	public async Task<ActionResult<TimeSpan>> GetHoursSumByUser(uint id) {
		return Repository.Flights.Where(f => f.UserId == id) switch {
			Flight flight => Ok(flight.Duration.TotalHours),
			_ => NotFound(),
		};
	}

	[HttpGet("hours/weeklySum")]
	public async Task<ActionResult<TimeSpan>> GetWeeklyHoursSum() {
		DateTime now = DateTime.Now;
		DateTime startOfWeek = now.AddDays(-1 * (int)now.DayOfWeek);
		DateTime endOfWeek = startOfWeek.AddDays(7);

		return Ok(await Repository.Flights
			.Where(f => f.DateTime >= startOfWeek && f.DateTime < endOfWeek)
			.SumAsync(f => f.Duration.TotalHours));
	}

}