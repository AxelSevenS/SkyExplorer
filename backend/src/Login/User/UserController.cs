namespace SkyExplorer;

using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
	public async Task<ActionResult> Authenticate([FromForm] UserLoginDTO dto) {
		dto.Password = jwtOptions.HashPassword(dto.Password);
		return await Repository.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password) switch {
			User user => Ok(JsonSerializer.Serialize(jwtOptions.GenerateFrom(user).Write())),
			null => NotFound(),
		};
	}


	[Authorize]
	public override async Task<ActionResult<User>> Update(uint id, [FromForm] UserUpdateDTO dto) {
		if (! VerifyOwnershipOrRole(id, SkyExplorer.User.Roles.Admin, out ActionResult<User> error, out _, out User.Roles userRole))
			return error;

		if (userRole < SkyExplorer.User.Roles.Admin) {
			dto.Role = null;
		}

		return await base.Update(id, dto);
	}

	[Authorize]
	public override async Task<ActionResult<User>> Delete(uint id) {
		// In this case the User's owner is... himself so we check for Admin status or self-deleting
		if (! VerifyOwnershipOrRole(id, SkyExplorer.User.Roles.Admin, out ActionResult<User> error, out _, out _))
			return error;

		return await base.Delete(id);
	}
}