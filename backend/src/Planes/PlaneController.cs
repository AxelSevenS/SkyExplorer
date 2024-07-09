namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

[ApiController]
[Route("api/planes")]
public class PlaneController(AppDbContext context) : RegularController<Plane, PlaneSetupDTO, PlaneUpdateDTO>(context) {
	protected override DbSet<Plane> Set => Repository.Planes;


	[HttpGet("mean")]
	public async Task<ActionResult<double>> GetMeanAverage() {
		List<Plane> AvailablePlanes = await Repository.Planes.ToListAsync();
		return AvailablePlanes.Count == 0
			? 0
			: AvailablePlanes.Count(p => p.Status == Plane.Availability.Available ) / AvailablePlanes.Count;
	}

	[HttpPut("status/{id}")]
	public async Task<ActionResult<Plane>> GetPlaneStatus(int id) {
		return await Repository.Planes.FindAsync(id) switch {
			Plane p => Ok(p.Status),
			_ => NotFound()
		};

	

	}
	[HttpPut("count")]
	public async Task<ActionResult<int>> GetPlaneCount() {
		return await Repository.Planes.CountAsync();
	}
}