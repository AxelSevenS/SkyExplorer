namespace SkyExplorer;

using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/courses")]
public class CourseController(AppDbContext context) : TimeFrameController<Course, CourseSetupDto, CourseUpdateDto>(context) {
	private static readonly Expression<Func<Course, DateTime>> GetCourseDateTime = c => c.Flight.DateTime;

	protected override DbSet<Course> Set => Repository.Courses;
	protected override IQueryable<Course> GetQuery => Set
		.Include(c => c.Flight).ThenInclude(f => f.Plane)
		.Include(c => c.Flight).ThenInclude(f => f.User)
		.Include(c => c.Flight).ThenInclude(f => f.Overseer)
		.Include(c => c.Flight).ThenInclude(f => f.Bill);

	protected override Expression<Func<Course, DateTime>> GetDateTime => GetCourseDateTime;


	[HttpGet("user/{userId}")]
	public async Task<ActionResult<List<Course>>> GetForUser(uint userId, [FromQuery] TimeFrame timeFrame = TimeFrame.AllTime, int offset = 0) {
		return Ok(await GetQuery
			.Where(c =>
				c.Flight.UserId == userId && c.Flight.User.Role == AppUser.Roles.User ||
				c.Flight.OverseerId == userId && c.Flight.Overseer.Role == AppUser.Roles.Collaborator
			)
			.InTimeFrame(c => c.Flight.DateTime, timeFrame, offset)
			.OrderByDescending(c => c.Flight.DateTime)
			.ToListAsync()
		);
	}


	[HttpGet("tesqsdqsdt/{userId}")]
	public async Task<ActionResult<List<Course>>> GetForUserSdq(uint userId, [FromQuery] TimeFrame timeFrame = TimeFrame.AllTime, int offset = 0) {
		return Ok(await GetQuery
			.Where(c =>
				c.Flight.UserId == userId && c.Flight.User.Role == AppUser.Roles.User ||
				c.Flight.OverseerId == userId && c.Flight.Overseer.Role == AppUser.Roles.Collaborator
			)
			.InTimeFrame(c => c.Flight.DateTime, timeFrame, offset)
			.OrderByDescending(c => c.Flight.DateTime)
			.ToListAsync()
		);
	}


	[HttpGet("time")]
	public async Task<ActionResult<TimeSpan>> GetTime([FromQuery] TimeFrame timeFrame = TimeFrame.AllTime, int offset = 0){
		return Ok(GetQuery
			.InTimeFrame(c => c.Flight.DateTime, timeFrame, offset)
			.Select(c => c.Flight.Duration)
			.ToList()
			.Aggregate(TimeSpan.Zero, (sum, d) => sum.Add(d))
		);
	}

	[HttpGet("count")]
	public async Task<ActionResult<int>> GetCount(){
		return Ok(await GetQuery.CountAsync());
	}

	// [HttpGet("info/{userId}")]
	// public async Task<ActionResult<List<Course>>> GetCourseInfo(uint userId){
	// 	return Ok(await GetQuery
	// 		.Where(c => c.Flight != null && c.Flight.UserId == userId)
	// 		.Select(c => new { c.Goals, c.AchievedGoals, c.Notes, c.AcquiredSkills })
	// 		.ToListAsync()
	// 	);
	// }

	// [HttpGet("StudentInfo/{userId}")]
	// public async Task<ActionResult<List<Course>>> GetStudentInfo(uint userId){
	// 	return Ok(await GetQuery
	// 		.Where(c => c.Flight != null && c.Flight.UserId == userId)
	// 		.Select(c => new { c.Goals, c.AchievedGoals })
	// 		.ToListAsync()
	// 	);
	// }


	[Authorize]
	public override async Task<ActionResult<Course>> Update(uint id, [FromForm] CourseUpdateDto dto) {
		Course? found = await Repository.Courses
			.Include(c => c.Flight)
			.FirstOrDefaultAsync(c => c.Id == id);
		if (found is null) return NotFound();

		if (! VerifyOwnershipOrRole(found.Flight.OverseerId, AppUser.Roles.Staff, out ActionResult<Course> result, out _, out _)) return result;

		return await base.Update(id, dto);
	}

	[Authorize]
	public override async Task<ActionResult<Course>> Delete(uint id) {
		Course? found = await Repository.Courses
			.Include(c => c.Flight)
			.FirstOrDefaultAsync(c => c.Id == id);
		if (found is null) return NotFound();

		if (! VerifyOwnershipOrRole(found.Flight.OverseerId, AppUser.Roles.Staff, out ActionResult<Course> result, out _, out _)) return result;

		return await base.Delete(id);
	}

	[Authorize]
	public override async Task<ActionResult<Course>> Add([FromForm] CourseSetupDto dto) {
		if (! VerifyRole(AppUser.Roles.Staff, out _)) return Unauthorized();

		return await base.Add(dto);
	}


}