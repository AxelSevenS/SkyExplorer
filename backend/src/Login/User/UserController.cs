namespace SkyExplorer;

using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

[ApiController]
[Route("api/users")]
public class UserController(AppDbContext repo, JwtOptions jwtOptions) : Controller<User, UserDTO>(repo) {
	/// <summary>
	/// Get all users
	/// </summary>
	/// <returns>
	/// All users
	/// </returns>
	[HttpGet]
	public async Task<List<User>> GetAll() =>
		await Repository.Users.ToListAsync();

	/// <summary>
	/// Get a user by id
	/// </summary>
	/// <param name="id">The id of the user</param>
	/// <returns>
	/// The user with the given id
	/// </returns>
	[HttpGet("{id}")]
	public async Task<ActionResult<User>> GetById(uint id) =>
		await Repository.Users.FindAsync(id) switch {
			User user => Ok(user),
			null => NotFound(),
		};

	/// <summary>
	/// Authenticate a user
	/// </summary>
	/// <param name="email">The Email of the user</param>
	/// <param name="password">The password of the user</param>
	/// <returns>
	/// The JWT token of the user,
	///     or NotFound if the user does not exist
	/// </returns>
	[HttpPost("auth")]
	public async Task<ActionResult> AuthenticateUser([FromForm] string email, [FromForm] string password) {
		password = jwtOptions.HashPassword(password);
		return await Repository.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password) switch {
			User user => Ok(JsonSerializer.Serialize(jwtOptions.GenerateFrom(user).Write())),
			null => NotFound(),
		};
	}

	/// <summary>
	/// Register a user
	/// </summary>
	/// <param name="email">The Email of the user</param>
	/// <param name="password">The password of the user</param>
	/// <returns>
	/// The user,
	///    or BadRequest if the user already exists
	/// </returns>
	[HttpPut]
	public ActionResult<User> RegisterUser([FromForm] string email, [FromForm] string password) {
		EntityEntry<User>? result = Repository.Users.Add(
			new() {
				Email = email,
				Password = jwtOptions.HashPassword(password)
			}
		);

		if (result.Entity is not User user) {
			return BadRequest();
		}

		Repository.SaveChanges();
		return Ok(user);
	}

	/// <summary>
	/// Update a user
	/// </summary>
	/// <param name="id">The id of the user</param>
	/// <param name="dto">The user info to update</param>
	/// <returns>
	/// The updated user
	/// </returns>
	[HttpPatch("{id}")]
	[Authorize]
	public async Task<ActionResult<User>> UpdateUser(uint id, [FromForm] UserDTO dto) {
		if (! VerifyOwnershipOrAuthZ(id, SkyExplorer.User.Authorizations.EditAnyUser, out ActionResult<User> error))
			return error;

		User? user = await Repository.Users.FindAsync(id);
		if (user is null) return NotFound();

		user.Update(dto);

		if (dto.Auth is User.Authorizations auths && VerifyAuthorization(SkyExplorer.User.Authorizations.EditUserAuths | auths)) {
			user.Auth = auths;
		}

		Repository.SaveChanges();
		return Ok(user);
	}

	/// <summary>
	/// Update a user's authorizations
	/// </summary>
	/// <param name="id">The id of the user</param>
	/// <param name="authorizations">The authorizations to set for the user</param>
	/// <returns>
	/// The updated user
	/// </returns>
	[HttpPut("auths/{id}")]
	[Authorize]
	public async Task<ActionResult<User>> UpdateUserAuths(uint id, [FromForm] User.Authorizations authorizations) {
		// Cannot give authorizations you do not have;
		// in principle, someone who can edit auths will be Admin (and as such, have all rights), but we check just in case.
		if (! VerifyOwnershipOrAuthZ(id, authorizations | SkyExplorer.User.Authorizations.EditUserAuths, out ActionResult<User> error))
			return error;

		User? current = await Repository.Users.FindAsync(id);
		if (current is null) {
			return NotFound();
		}
		current.Auth = authorizations;

		Repository.SaveChanges();
		return Ok(current);
	}

	/// <summary>
	/// Delete a user
	/// </summary>
	/// <param name="id">The id of the user</param>
	/// <returns>
	/// The deleted user
	/// </returns>
	[HttpDelete("{id}")]
	[Authorize]
	public async Task<ActionResult<User>> DeleteUser(uint id) {
		if (! VerifyOwnershipOrAuthZ(id, SkyExplorer.User.Authorizations.DeleteAnyUser, out ActionResult<User> error))
			return error;

		User? current = await Repository.Users.FindAsync(id);
		if (current is null) return NotFound();

		EntityEntry<User> deleted = Repository.Users.Remove(current);

		Repository.SaveChanges();
		return Ok(deleted.Entity);
	}
}