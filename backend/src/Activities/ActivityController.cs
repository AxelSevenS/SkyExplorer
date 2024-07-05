namespace SkyExplorer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/activities")]
public class ActivityController(AppDbContext repo) : Controller<Activity, ActivitySetupDTO, ActivityUpdateDTO>(repo) {
	protected override DbSet<Activity> Set => Repository.Activities;
}