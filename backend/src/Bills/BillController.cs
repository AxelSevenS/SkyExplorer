namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/bills")]
public class BillController(AppDbContext repo) : Controller<Bill, BillCreateDTO, BillUpdateDTO>(repo) {
	protected override DbSet<Bill> Set => Repository.Bills;


	// [HttpGet("searchByName")]
	// public async Task<ActionResult<List<Bill>>> SearchByName([FromQuery] string name) {
	// 	var bills = await Repository.Bills.Where(b => b.Name.Contains(name)).ToListAsync();
	// 	return Ok(bills);
	// }
}