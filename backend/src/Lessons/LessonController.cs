namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/lessons")]
public class LessonController(AppDbContext repo) : Controller<Lesson, LessonDTO>(repo) {
	[HttpGet]
	public Task<List<Lesson>> GetAll() =>
		Repository.Lessons.ToListAsync();

	[HttpGet("{id}")]
	public ActionResult<Lesson> Get(int id) =>
		Repository.Lessons.Find(id) switch {
			Lesson lesson => Ok(lesson),
			null => NotFound(),
		};

	[HttpPost]
	public async Task<ActionResult<Lesson>> AddLesson([FromForm] LessonDTO dto) =>
		Ok(await Repository.Lessons.AddAsync(new(dto)));
}