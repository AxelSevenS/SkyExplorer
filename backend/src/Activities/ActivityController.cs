namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/activities")]
public class ActivityController(AppDbContext context) : Controller<Activity, ActivitySetupDTO, ActivityUpdateDTO>(context) {
	protected override DbSet<Activity> Set => Repository.Activities;
	protected override IQueryable<Activity> GetQuery => Set
		.Include(a => a.Flight)
		.Include(c => c.Flight).ThenInclude(f => f.Plane)
		.Include(c => c.Flight).ThenInclude(f => f.Overseer)
		.Include(c => c.Flight).ThenInclude(f => f.User);
}