namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

[ApiController]
[Route("api/Planes")]
public class PlaneController(AppDbContext repo) : Controller<Plane, PlaneCreateDTO, PlaneUpdateDTO>(repo) {
	[HttpGet]
	public Task<List<Plane>> GetAll() =>
		Repository.Planes.ToListAsync();

	[HttpGet("{id}")]
	public ActionResult<Plane> Get(int id) =>
		Repository.Planes.Find(id) switch {
			Plane plane => Ok(plane),
			null => NotFound(),
		};

	[HttpPost]
	public async Task<ActionResult<Plane>> AddPlane([FromForm] PlaneCreateDTO dto) =>
		Ok(await Repository.Planes.AddAsync(new(dto)));
}