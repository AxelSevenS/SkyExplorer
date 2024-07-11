namespace SkyExplorer;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/bills")]
public class BillController(AppDbContext context) : RegularController<Bill, BillSetupDto, BillUpdateDto>(context) {
	protected override DbSet<Bill> Set => Repository.Bills;
	protected override IQueryable<Bill> GetQuery {
		get {
			if (! TryGetAuthenticatedUserId(out uint requesterId)) {
				return Enumerable.Empty<Bill>().AsQueryable();
			}

			if (VerifyRole(AppUser.Roles.Staff, out _)) return Repository.Bills
				.Include(f => f.User);

			return Repository.Bills
				.Include(f => f.User)
				.Where(f => requesterId == f.UserId);
		}
	}

	[HttpGet("search")]
	public async Task<ActionResult<IEnumerable<Bill>>> Search([FromForm] string name) =>
		Ok(await GetQuery
			.Where(b => b.Name
			.Contains(name))
			.ToListAsync()
		);

	[HttpGet("ordered")]
	public async Task<ActionResult<IEnumerable<Bill>>> GetOrdered() =>
		Ok(await GetQuery
			.OrderByDescending(b => b.CreatedAt)
			.ToListAsync()
		);

	[HttpGet("ordered/{userId}")]
	public async Task<ActionResult<IEnumerable<Bill>>> Order(uint userId) =>
		Ok(await Repository.Bills
			.Include(b => b.User)
			.Where(b => b.UserId == userId)
			.OrderByDescending(b => b.CreatedAt)
			.ToListAsync()
		);


	[Authorize]
	public override async Task<ActionResult<Bill>> Update(uint id, [FromForm] BillUpdateDto dto) {
		if (! VerifyRole(AppUser.Roles.Staff, out _)) {
			dto.WasAcquitted = null;
		}
		// Bill? found = await Repository.Bills
		// 	.Include(b => b.User)
		// 	.FirstOrDefaultAsync(f => f.UserId == id);
		// if (found is null) return NotFound();

		// if (! VerifyOwnershipOrRole(found.UserId, AppUser.Roles.Staff, out ActionResult<Bill> result, out _, out _)) return result;

		return await base.Update(id, dto);
	}

	[Authorize]
	public override async Task<ActionResult<Bill>> Delete(uint id) {
		// Bill? found = await Repository.Bills
		// 	.Include(b => b.User)
		// 	.FirstOrDefaultAsync(f => f.UserId == id);
		// if (found is null) return NotFound();

		// if (! VerifyOwnershipOrRole(found.UserId, AppUser.Roles.Staff, out ActionResult<Bill> result, out _, out _)) return result;

		return await base.Delete(id);
	}

	[Authorize]
	public override async Task<ActionResult<Bill>> Add([FromForm] BillSetupDto dto) {
		if (! VerifyRole(AppUser.Roles.Collaborator, out _)) return Unauthorized();

		return await base.Add(dto);
	}
}