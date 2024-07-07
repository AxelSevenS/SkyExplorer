namespace SkyExplorer;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/flights")]
public class FlightController(AppDbContext repo) : Controller<Flight, FlightSetupDTO, FlightUpdateDTO>(repo) {
	protected override DbSet<Flight> Set => Repository.Flights;
	protected override IQueryable<Flight> GetQuery => Set.Include(c => c.Plane).Where(f => f.Overseer.Role >= AppUser.Roles.Collaborator && f.User.Role == AppUser.Roles.User);


	[HttpGet("time")]
	public async Task<ActionResult<TimeSpan>> GetFlightTime() {
		return Ok(GetQuery
			.Select(f => f.Duration)
			.ToList()
			.Aggregate(TimeSpan.Zero, (sum, d) => sum.Add(d))
		);
	}

	[HttpGet("time/{userId}")]
	public async Task<ActionResult<TimeSpan>> GetFlightTimeForUser(uint userId) {
		return Ok(GetQuery
			.Where(f => f.UserId == userId)
			.Select(f => f.Duration)
			.ToList()
			.Aggregate(TimeSpan.Zero, (sum, d) => sum.Add(d))
		);
	}

	[HttpGet("time/weekly")]
	public async Task<ActionResult<TimeSpan>> GetWeeklyFlightTime([FromQuery] int offset = 0) {
		(DateOnly monday, DateOnly sunday) = Utility.GetWeekSpan(offset);

		return Ok(GetQuery
			.Where(f => DateOnly.FromDateTime(f.DateTime) >= monday && DateOnly.FromDateTime(f.DateTime) <= sunday)
			.Select(f => f.Duration)
			.ToList()
			.Aggregate(TimeSpan.Zero, (sum, d) => sum.Add(d))
		);
	}
	[HttpGet("time/weekly/{userId}")]
	public async Task<ActionResult<TimeSpan>> GetWeeklyFlightTimeForUser(uint userId, [FromQuery] int offset = 0) {
		(DateOnly monday, DateOnly sunday) = Utility.GetWeekSpan(offset);

		return Ok(GetQuery
			.Where(f => f.UserId == userId && DateOnly.FromDateTime(f.DateTime) >= monday && DateOnly.FromDateTime(f.DateTime) <= sunday)
			.Select(f => f.Duration)
			.ToList()
			.Aggregate(TimeSpan.Zero, (sum, d) => sum.Add(d))
		);
	}

}