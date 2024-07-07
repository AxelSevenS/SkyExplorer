namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/courses")]
public class CourseController(AppDbContext repo) : Controller<Course, CourseSetupDTO, CourseUpdateDTO>(repo) {
	protected override DbSet<Course> Set => Repository.Courses;
	protected override IQueryable<Course> GetQuery => Set.Include(c => c.Flight).ThenInclude(f => f.Plane);


	[HttpGet("forUser/{userId}")]
	public async Task<ActionResult<List<Course>>> GetForUser(uint userId) {
		return Ok(await GetQuery
			.Where(c =>
				c.Flight != null &&
				c.Flight.UserId == userId
			)
			.ToListAsync()
		);
	}

	[HttpGet("weekly/{userId}")]
	public async Task<ActionResult<List<Course>>> GetWeeklyForUser(uint userId, [FromQuery] int offset = 0) {
		(DateOnly monday, DateOnly sunday) = Utility.GetWeekSpan(offset, false);

		return Ok(await GetQuery
			.Where(c =>
				c.Flight != null &&
				c.Flight.UserId == userId &&
				DateOnly.FromDateTime(c.Flight.DateTime) >= monday && DateOnly.FromDateTime(c.Flight.DateTime) <= sunday
			)
			.ToListAsync()
		);
	}
}