namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/bills")]
public class BillController(AppDbContext context) : RegularController<Bill, BillSetupDTO, BillUpdateDTO>(context) {
	protected override DbSet<Bill> Set => Repository.Bills;


	[HttpGet("search")]
	public async Task<ActionResult<IEnumerable<Bill>>> Search([FromForm] string name ) =>
		Ok(await GetQuery
			.Where(b => b.Name
			.Contains(name))
			.ToListAsync()
		);

	[HttpGet("order")]
	public async Task<ActionResult<IEnumerable<Bill>>> All() =>
		Ok(await GetQuery
		.OrderByDescending(b => b.CreatedAt)
		.ToListAsync());

	[HttpGet("order/{userId}")]
	public async Task<ActionResult<IEnumerable<Bill>>> Order(int userId) =>
		Ok(await GetQuery
		.Join(context.Flights,
			bill => bill.Id,
			flight => flight.Id,
			(bill, flight) => new { Bill = bill, Flight = flight })
		.Where(bf => bf.Flight.UserId == userId)
		.Select(bf => bf.Bill)
		.ToListAsync());
}