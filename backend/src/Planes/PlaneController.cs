namespace SkyExplorer;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

[ApiController]
[Route("api/planes")]
public class PlaneController(AppDbContext context) : RegularController<Plane, PlaneSetupDto, PlaneUpdateDto>(context) {
	protected override DbSet<Plane> Set => Repository.Planes;


	[HttpGet("mean")]
	public async Task<ActionResult<double>> GetMeanAverage() {
		List<Plane> AvailablePlanes = await Repository.Planes.ToListAsync();
		return AvailablePlanes.Count == 0
			? 0
			: AvailablePlanes.Count(p => p.Status == Plane.Availability.Available) / AvailablePlanes.Count;
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


	[Authorize]
	public override async Task<ActionResult<Plane>> Update(uint id, [FromForm] PlaneUpdateDto dto) {
		if (!VerifyRole(AppUser.Roles.Staff, out _)) return Unauthorized();

		return await base.Update(id, dto);
	}

	[Authorize]
	public override async Task<ActionResult<Plane>> Delete(uint id) {
		if (!VerifyRole(AppUser.Roles.Staff, out _)) return Unauthorized();

		return await base.Delete(id);
	}

	[Authorize]
	public override async Task<ActionResult<Plane>> Add([FromForm] PlaneSetupDto dto) {
		if (!VerifyRole(AppUser.Roles.Staff, out _)) return Unauthorized();

		return await base.Add(dto);
	}
}