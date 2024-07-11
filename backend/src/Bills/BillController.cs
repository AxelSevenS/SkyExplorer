namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/bills")]
public class BillController(AppDbContext context) : RegularController<Bill, BillSetupDTO, BillUpdateDTO>(context) {
	protected override DbSet<Bill> Set => Repository.Bills;

	[HttpGet("search")]
	public async Task<ActionResult<IEnumerable<Bill>>> Search([FromForm] string name) =>
		Ok(await GetQuery
			.Where(b => b.Name
			.Contains(name))
			.ToListAsync()
		);

	[HttpGet("ordered")]
	public async Task<ActionResult<IEnumerable<Bill>>> All() =>
		Ok(await GetQuery
			.OrderByDescending(b => b.CreatedAt)
			.ToListAsync()
		);

	[HttpGet("ordered/{userId}")]
	public async Task<ActionResult<IEnumerable<Bill>>> Order(uint userId) =>
		Ok(await Repository.Flights
			.Include(c => c.Plane)
			.Include(c => c.User)
			.Include(c => c.Overseer)
			.Include(c => c.Bill)
			.Where(b => b.UserId == userId)
			.Select(b => b.Bill)
			.OrderByDescending(b => b.CreatedAt)
			.ToListAsync()
		);
}