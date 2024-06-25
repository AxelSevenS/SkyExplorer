namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

[ApiController]
[Route("api/flights")]
public class FlightController(AppDbContext repo) : Controller<Flight, FlightCreateDTO, FlightUpdateDTO>(repo) {
	[HttpGet]
	public Task<List<Flight>> GetAll() =>
		Repository.Flights.ToListAsync();

	[HttpGet("{id}")]
	public ActionResult<Flight> GetById(uint id) =>
		Repository.Flights.Find(id) switch {
			Flight flight => Ok(flight),
			null => NotFound(),
		};

	[HttpPost]
	public async Task<ActionResult<Flight>> AddFlight([FromForm] FlightCreateDTO dto) =>
		Ok(await Repository.Flights.AddAsync(new(dto)));

	[HttpPatch("{id}")]
	public async Task<ActionResult<Flight>> UpdateFlight(uint id, [FromForm] FlightUpdateDTO dto) {
		Flight? found = Repository.Flights.Find(id);
		if (found is null) {
			return NotFound();
		}

		found.Update(dto);

		Repository.SaveChanges();
		return Ok(found);
	}


	[HttpDelete("{id}")]
	public async Task<ActionResult<Flight>> DeleteFlight(uint id) {
		Flight? found = Repository.Flights.Find(id);
		if (found is null) {
			Console.WriteLine("Flight not found");
			return NotFound();

		}

		Repository.Flights.Remove(found);
		Repository.SaveChanges();
		return Ok(found);
	}
}