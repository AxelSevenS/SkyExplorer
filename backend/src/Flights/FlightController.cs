namespace SkyExplorer;

using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/flights")]
public class FlightController(AppDbContext context) : TimeFrameController<Flight, FlightSetupDto, FlightUpdateDto>(context) {
	private static readonly Expression<Func<Flight, DateTime>> GetFlightDateTime = f => f.DateTime;

	protected override DbSet<Flight> Set => Repository.Flights;
	protected override IQueryable<Flight> GetQuery => Set
		.Include(c => c.Plane)
		.Include(c => c.User)
		.Include(c => c.Overseer)
		.Include(c => c.Bill);

	protected override Expression<Func<Flight, DateTime>> GetDateTime => GetFlightDateTime;

	[HttpGet("time")]
	public async Task<ActionResult<TimeSpan>> GetWeeklyFlightTime([FromQuery] TimeFrame timeFrame = TimeFrame.AllTime, [FromQuery] DateFrame dateFrame = DateFrame.AllTime, [FromQuery] int offset = 0) {
		return Ok(GetQuery
			.InTimeFrame(f => f.DateTime, timeFrame, offset)
			.InDateFrame(f => f.DateTime, dateFrame, offset)
			.Select(f => f.Duration)
			.ToList()
			.Aggregate(TimeSpan.Zero, (sum, d) => sum.Add(d))
		);
	}

	[HttpGet("time/{userId}")]
	public async Task<ActionResult<TimeSpan>> GetWeeklyFlightTimeForUser(uint userId, [FromQuery] TimeFrame timeFrame = TimeFrame.AllTime, [FromQuery] DateFrame dateFrame = DateFrame.AllTime, [FromQuery] int offset = 0) {
		return Ok(GetQuery
			.Where(f => f.UserId == userId)
			.InTimeFrame(f => f.DateTime, timeFrame, offset)
			.InDateFrame(f => f.DateTime, dateFrame, offset)
			.Select(f => f.Duration)
			.ToList()
			.Aggregate(TimeSpan.Zero, (sum, d) => sum.Add(d))
		);
	}


	[Authorize]
	public override async Task<ActionResult<Flight>> Update(uint id, [FromForm] FlightUpdateDto dto) {
		Flight? found = await Repository.Flights
			.FirstOrDefaultAsync(c => c.Id == id);
		if (found is null) return NotFound();

		if (! VerifyOwnershipOrRole(found.OverseerId, AppUser.Roles.Staff, out ActionResult<Flight> result, out _, out _)) return result;

		return await base.Update(id, dto);
	}

	[Authorize]
	public override async Task<ActionResult<Flight>> Delete(uint id) {
		Flight? found = await Repository.Flights
			.FirstOrDefaultAsync(c => c.Id == id);
		if (found is null) return NotFound();

		if (! VerifyOwnershipOrRole(found.OverseerId, AppUser.Roles.Staff, out ActionResult<Flight> result, out _, out _)) return result;

		return await base.Delete(id);
	}

	[Authorize]
	public override async Task<ActionResult<Flight>> Add([FromForm] FlightSetupDto dto) {
		if (! VerifyRole(AppUser.Roles.Collaborator, out _)) return Unauthorized();

		return await base.Add(dto);
	}
}