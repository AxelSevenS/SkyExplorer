namespace SkyExplorer;

using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

[ApiController]
[Route("api/users")]
public class UserController(AppDbContext repo, JwtOptions jwtOptions) : Controller<User, UserRegisterDTO, UserUpdateDTO>(repo) {
	protected override DbSet<User> Set => Repository.Users;


	/// <summary>
	/// Authenticate a user
	/// </summary>
	/// <param name="dto">The Login info of the user</param>
	/// <returns>
	/// The JWT token of the user,
	///     or NotFound if the user does not exist
	/// </returns>
	[HttpPost("auth")]
	public async Task<ActionResult> AuthenticateUser([FromForm] UserLoginDTO dto) {
		dto.Password = jwtOptions.HashPassword(dto.Password);
		return await Repository.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password) switch {
			User user => Ok(JsonSerializer.Serialize(jwtOptions.GenerateFrom(user).Write())),
			null => NotFound(),
		};
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
		if (!VerifyOwnershipOrAuthZ(id, authorizations | SkyExplorer.User.Authorizations.EditUserAuths, out ActionResult<User> error))
			return error;

		User? current = await Repository.Users.FindAsync(id);
		if (current is null) {
			return NotFound();
		}
		current.Auth = authorizations;

		Repository.SaveChanges();
		return Ok(current);
	}


	[Authorize]
	public override async Task<ActionResult<User>> Update(uint id, [FromForm] UserUpdateDTO dto) {
		if (! VerifyOwnershipOrAuthZ(id, SkyExplorer.User.Authorizations.EditAnyUser, out ActionResult<User> error))
			return error;

		return await base.Update(id, dto);
	}

	[Authorize]
	public override async Task<ActionResult<User>> Delete(uint id) {
		if (! VerifyOwnershipOrAuthZ(id, SkyExplorer.User.Authorizations.DeleteAnyUser, out ActionResult<User> error))
			return error;

		return await base.Delete(id);
	}
}