namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

[ApiController]
[Route("api/planes")]
public class PlaneController(AppDbContext repo) : Controller<Plane, PlaneSetupDTO, PlaneUpdateDTO>(repo) {
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
			Plane p => p.Status switch {
				Plane.Availability.Available => Ok(Plane.Availability.Unavailable),
				Plane.Availability.Unavailable => Ok(Plane.Availability.Available),
				Plane.Availability.Maintenance => Ok(Plane.Availability.Unavailable),
				_ => BadRequest(),
			},
			_ => NotFound()
		};
	}
}