namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

[ApiController]
[Route("api/activities")]
public class ActivityController(AppDbContext repo) : Controller<Activity, ActivityCreateDTO, ActivityUpdateDTO>(repo) {
	[HttpGet]
	public Task<List<Activity>> GetAll() =>
		Repository.Activities.ToListAsync();

	[HttpGet("{id}")]
	public ActionResult<Activity> Get(int id) =>
		Repository.Activities.Find(id) switch {
			Activity activity => Ok(activity),
			null => NotFound(),
		};

	[HttpPost]
	public async Task<ActionResult<Activity>> AddActivity([FromForm] ActivityCreateDTO dto) =>
		Ok(await Repository.Activities.AddAsync(new(dto)));
}