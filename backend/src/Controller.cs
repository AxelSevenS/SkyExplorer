namespace SkyExplorer;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public abstract class Controller<TData, TSetupDTO, TUpdateDTO>(AppDbContext repository) : ControllerBase where TData : class, IEntity<TData, TSetupDTO, TUpdateDTO> {
	protected readonly AppDbContext Repository = repository;
	protected abstract DbSet<TData> Set { get; }


	[HttpGet]
	public virtual Task<List<TData>> GetAll() =>
		Set.ToListAsync();

	[HttpGet("{id}")]
	public virtual ActionResult<TData> GetById(uint id) =>
		Set.Find(id) switch {
			TData flight => Ok(flight),
			null => NotFound(),
		};


	[HttpPost]
	public virtual async Task<ActionResult<TData>> Add([FromForm] TSetupDTO dto) =>
		Ok(await Set.AddAsync(TData.CreateFrom(dto)));


	[HttpPatch("{id}")]
	public virtual async Task<ActionResult<TData>> Update(uint id, [FromForm] TUpdateDTO dto) {
		TData? found = Set.Find(id);
		if (found is null) {
			return NotFound();
		}

		found.Update(dto);

		Repository.SaveChanges();
		return Ok(found);
	}


	[HttpDelete("{id}")]
	public virtual async Task<ActionResult<TData>> Delete(uint id) {
		TData? found = Set.Find(id);
		if (found is null) {
			return NotFound();
		}

		Set.Remove(found);

		Repository.SaveChanges();
		return Ok(found);
	}



	/// <summary>
	/// Check wether the user is authenticated and if the user holds the given <c>neededRole</c>
	/// </summary>
	/// <param name="neededRole"></param>
	/// <returns>True if the user is authenticated and holds the authorization(s), False if the user is not authenticated or doesn't hold the authorization(s)</returns>
	protected bool VerifyRole(User.Roles neededRole, out User.Roles role) {
		role = SkyExplorer.User.Roles.User;
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
	/// <typeparam name="T"></typeparam>
	/// <param name="neededRole">The authorizations the authenticated user needs to posess to validate the check</param>
	/// <param name="result">The StatusCodeResult corresponding to the error, if the function returns False</param>
	/// <param name="userId">The Id of the currently authenticated user, if the function returns True</param>
	/// <returns>
	/// True if the user is authenticated and posesses the given <c>authorizations</c>, otherwise False.
	/// </returns>
	protected bool VerifyRole<T>(User.Roles neededRole, out ActionResult<T> result, out uint userId, out User.Roles userRole) where T : class {
		result = null!;
		userRole = SkyExplorer.User.Roles.User;

		if (! TryGetAuthenticatedUserId(out userId)) {
			result = Unauthorized();
			return false;
		}
		if (! VerifyRole(neededRole, out userRole)) {
			result = Forbid();
			return false;
		}

		return true;
	}

	/// <summary>
	/// Verifies wether the current authenticated user has the given id.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="authId">The Id that the authenticated user needs to have to be deemed "Owner" of the resource</param>
	/// <param name="result">The StatusCodeResult corresponding to the error if the verification was unsuccessful</param>
	/// <returns>
	/// True if the user is authenticated and posesses the given <c>authorizations</c> OR has <c>authId</c> as their Id, otherwise False.
	/// </returns>
	protected bool VerifyOwnership<T>(uint authId, out ActionResult<T> result) {
		if (! TryGetAuthenticatedUserId(out uint currentId)) {
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
	/// <typeparam name="T"></typeparam>
	/// <param name="authId">The Id that the authenticated user needs to have to be deemed "Owner" of the resource</param>
	/// <param name="neededRole">The authorizations the authenticated user needs to posess to validate the check and override the Ownership check</param>
	/// <param name="result">The StatusCodeResult corresponding to the error if the verification was unsuccessful</param>
	/// <returns>
	/// True if the user is authenticated and posesses the given <c>authorizations</c> OR has <c>authId</c> as their Id, otherwise False.
	/// </returns>
	protected bool VerifyOwnershipOrRole<T>(uint authId, User.Roles neededRole, out ActionResult<T> result, out uint userId, out User.Roles userRole) where T : class {
		result = null!;
		userRole = SkyExplorer.User.Roles.User;

		if (! TryGetAuthenticatedUserId(out userId)) {
			result = Unauthorized();
			return false;
		}
		if (authId != userId && ! VerifyRole(neededRole, out userRole)) {
			result = Forbid();
			return false;
		}

		return true;
	}
}