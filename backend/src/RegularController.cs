namespace SkyExplorer;

using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public abstract class RegularController<T, TSetupDTO, TUpdateDTO>(AppDbContext context) : Controller<T, TSetupDTO, TUpdateDTO>(context) where T : class, IEntity where TSetupDTO : class, IEntitySetup<T> where TUpdateDTO : class, IEntityUpdate<T> {
	[HttpGet]
	public virtual async Task<ActionResult<List<T>>> GetAll() =>
		Ok(await GetQuery.ToListAsync());
}