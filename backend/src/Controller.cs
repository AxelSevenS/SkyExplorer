namespace SkyExplorer;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public abstract class Controller<T, TSetupDto, TUpdateDto>(AppDbContext context) : ControllerBase where T : class, IEntity where TSetupDto : class, IEntitySetup<T> where TUpdateDto : class, IEntityUpdate<T> {
	protected readonly AppDbContext Repository = context;
	protected abstract DbSet<T> Set { get; }

	protected virtual IQueryable<T> GetQuery => Set;


	[HttpGet("{id}")]
	public virtual async Task<ActionResult<T>> GetById(uint id) =>
		await GetQuery
			.FirstAsync(e => e.Id == id) switch {
				T flight => Ok(flight),
				null => NotFound(),
			};


	[HttpPost]
	public virtual async Task<ActionResult<T>> Add([FromForm] TSetupDto dto) {
		T? entity = dto.Create(Repository, out string error);
		if (entity is null) {
			return BadRequest(error);
		}

		EntityEntry<T> entry = await Set.AddAsync(entity);

		await Repository.SaveChangesAsync();
		return Ok(entry.Entity);
	}


	[HttpPatch("{id}")]
	public virtual async Task<ActionResult<T>> Update(uint id, [FromForm] TUpdateDto dto) {
		if (GetQuery.FirstOrDefault(e => e.Id == id) is not T found) {
			return NotFound();
		}

		if (!dto.TryUpdate(found, Repository, out string error)) {
			return BadRequest(error);
		}

		await Repository.SaveChangesAsync();
		return Ok(found);
	}


	[HttpDelete("{id}")]
	public virtual async Task<ActionResult<T>> Delete(uint id) {
		T? found = await GetQuery
			.FirstOrDefaultAsync(e => e.Id == id);
		if (found is null) {
			return NotFound();
		}

		Set.Remove(found);

		await Repository.SaveChangesAsync();
		return Ok(found);
	}



	/// <summary>
	/// Check wether the user is authenticated and if the user holds the given <c>neededRole</c>
	/// </summary>
	/// <param name="neededRole"></param>
	/// <returns>True if the user is authenticated and holds the authorization(s), False if the user is not authenticated or doesn't hold the authorization(s)</returns>
	protected bool VerifyRole(AppUser.Roles neededRole, out AppUser.Roles role) {
		role = AppUser.Roles.User;
		if (
			HttpContext.User.FindFirst(JwtOptions.RoleClaim)?.Value is string roleClaim &&
			Enum.TryParse(roleClaim, true, out role)
		) {
			return (int)role >= (int)neededRole;
		}

		return false;
	}

	/// <summary>
	/// Verifies wether the current authenticated user has the given authorizations, if not <c>result</c> will contain a StatusCodeResult corresponding to the error.
	/// </summary>
	/// <param name="neededRole">The authorizations the authenticated user needs to posess to validate the check</param>
	/// <param name="result">The StatusCodeResult corresponding to the error, if the function returns False</param>
	/// <param name="userId">The Id of the currently authenticated user, if the function returns True</param>
	/// <returns>
	/// True if the user is authenticated and posesses the given <c>authorizations</c>, otherwise False.
	/// </returns>
	protected bool VerifyRole(AppUser.Roles neededRole, out ActionResult<T> result, out uint userId, out AppUser.Roles userRole) {
		result = null!;
		userRole = AppUser.Roles.User;

		if (!TryGetAuthenticatedUserId(out userId)) {
			result = Unauthorized();
			return false;
		}
		if (!VerifyRole(neededRole, out userRole)) {
			result = Forbid();
			return false;
		}

		return true;
	}

	/// <summary>
	/// Verifies wether the current authenticated user has the given id.
	/// </summary>
	/// <param name="authId">The Id that the authenticated user needs to have to be deemed "Owner" of the resource</param>
	/// <param name="result">The StatusCodeResult corresponding to the error if the verification was unsuccessful</param>
	/// <returns>
	/// True if the user is authenticated and posesses the given <c>authorizations</c> OR has <c>authId</c> as their Id, otherwise False.
	/// </returns>
	protected bool VerifyOwnership(uint authId, out ActionResult<T> result) {
		if (!TryGetAuthenticatedUserId(out uint currentId)) {
			result = Unauthorized();
			return false;
		}
		if (authId != currentId) {
			result = Forbid();
			return false;
		}

		result = null!;
		return true;
	}

	/// <summary>
	/// Verifies wether the user is authenticated and if it is, set <c>id</c> to the authenticated user's Id.
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	protected bool TryGetAuthenticatedUserId(out uint id) {
		id = 0;
		if (
			HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub) is Claim claim &&
			uint.TryParse(Encoding.UTF8.GetBytes(claim.Value), out id)
		) {
			return id != 0;
		}

		return false;
	}

	/// <summary>
	/// Verify if the current authenticated user exists and has the given Id as its own
	/// </summary>
	/// <param name="validId"></param>
	/// <returns>True if the Authenticated user exists and has the given Id, False if the user is not authenticated or doesn't fit the given Id</returns>
	protected bool VerifyAuthenticatedId(uint validId) =>
		TryGetAuthenticatedUserId(out uint authenticatedId) && authenticatedId == validId;

	/// <summary>
	/// Verifies wether the current authenticated user has the given authorizations, if not <c>result</c> will contain a StatusCodeResult corresponding to the error.
	/// </summary>
	/// <param name="authId">The Id that the authenticated user needs to have to be deemed "Owner" of the resource</param>
	/// <param name="neededRole">The authorizations the authenticated user needs to posess to validate the check and override the Ownership check</param>
	/// <param name="result">The StatusCodeResult corresponding to the error if the verification was unsuccessful</param>
	/// <returns>
	/// True if the user is authenticated and posesses the given <c>authorizations</c> OR has <c>authId</c> as their Id, otherwise False.
	/// </returns>
	protected bool VerifyOwnershipOrRole(uint authId, AppUser.Roles neededRole, out ActionResult<T> result, out uint userId, out AppUser.Roles userRole) {
		result = null!;
		userRole = AppUser.Roles.User;

		if (!TryGetAuthenticatedUserId(out userId)) {
			result = Unauthorized();
			return false;
		}
		if (authId != userId && !VerifyRole(neededRole, out userRole)) {
			result = Forbid();
			return false;
		}

		return true;
	}
}