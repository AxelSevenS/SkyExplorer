namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/lessons")]
public class LessonController(AppDbContext repo) : Controller<Lesson, LessonSetupDTO, LessonUpdateDTO>(repo) {
	protected override DbSet<Lesson> Set => Repository.Lessons;
}