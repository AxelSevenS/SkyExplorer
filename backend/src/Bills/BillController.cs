namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

[ApiController]
[Route("api/bills")]
public class BillController(AppDbContext repo) : Controller<Bill, BillDTO>(repo) {
	[HttpGet]
	public Task<List<Bill>> GetAll() =>
		Repository.Bills.ToListAsync();

	[HttpGet("{id}")]
	public ActionResult<Bill> Get(int id) =>
		Repository.Bills.Find(id) switch {
			Bill bill => Ok(bill),
			null => NotFound(),
		};

	[HttpPost]
	public async Task<ActionResult<Bill>> AddBill([FromForm] BillDTO dto) =>
		Ok(await Repository.Bills.AddAsync(new(dto)));
}