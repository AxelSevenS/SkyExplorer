namespace SkyExplorer;

using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public abstract class RegularController<T, TSetupDto, TUpdateDto>(AppDbContext context) : Controller<T, TSetupDto, TUpdateDto>(context) where T : class, IEntity where TSetupDto : class, IEntitySetup<T> where TUpdateDto : class, IEntityUpdate<T> {
	[HttpGet]
	public virtual async Task<ActionResult<List<T>>> GetAll() =>
		Ok(await GetQuery.ToListAsync());
}