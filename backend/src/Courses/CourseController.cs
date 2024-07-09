namespace SkyExplorer;

using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/courses")]
public class CourseController(AppDbContext context) : TimeFrameController<Course, CourseSetupDTO, CourseUpdateDTO>(context) {
	private static readonly Expression<Func<Course, DateTime>> GetCourseDateTime = c => c.Flight.DateTime;

	protected override DbSet<Course> Set => Repository.Courses;
	protected override IQueryable<Course> GetQuery => Set
		.Include(c => c.Flight).ThenInclude(f => f.Plane)
		.Include(c => c.Flight).ThenInclude(f => f.User)
		.Include(c => c.Flight).ThenInclude(f => f.Overseer)
		.Include(c => c.Flight).ThenInclude(f => f.Bill);

	protected override Expression<Func<Course, DateTime>> GetDateTime => GetCourseDateTime;


	[HttpGet("student/{userId}")]
	public async Task<ActionResult<List<Course>>> GetForStudent(uint userId, [FromQuery] TimeFrame timeFrame = TimeFrame.AllTime, int offset = 0) {
		return Ok(await GetQuery
			.Where(c => c.Flight.UserId == userId)
			.InTimeFrame(c => c.Flight.DateTime, timeFrame, offset)
			.ToListAsync()
		);
	}

	[HttpGet("teacher/{userId}")]
	public async Task<ActionResult<List<Course>>> GetForTeacher(uint userId, [FromQuery] TimeFrame timeFrame = TimeFrame.AllTime, int offset = 0) {
		return Ok(await GetQuery
			.Where(c => c.Flight.OverseerId == userId)
			.InTimeFrame(c => c.Flight.DateTime, timeFrame, offset)
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

	[HttpGet("StudentInfo/{userId}")]
	public async Task<ActionResult<List<Course>>> GetStudentInfo(uint userId){
		return Ok(await GetQuery
			.Where(c => c.Flight != null && c.Flight.UserId == userId)
			.Select(c => new { c.Goals, c.AchievedGoals })
			.ToListAsync()
		);
	}


}