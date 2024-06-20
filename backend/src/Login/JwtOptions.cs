namespace SkyExplorer;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public record class JwtOptions {
	public const string Jwt = "Jwt";
	public const string RoleClaim = "roles";

	public string Issuer { get; set; } = string.Empty;
	public string Audience { get; set; } = string.Empty;
	public string SigningKey { get; set; } = string.Empty;
	public int ExpirationSeconds { get; set; } = 3600;


	public byte[] SigningKeyBytes => _signingKeyBytes ??= Encoding.ASCII.GetBytes(SigningKey);
	private byte[]? _signingKeyBytes;

	public SymmetricSecurityKey SecurityKey => _securityKey ??= new SymmetricSecurityKey(SigningKeyBytes);
	private SymmetricSecurityKey? _securityKey;



	public string HashPassword(string unhashed) {
		using HMACSHA256 hmac = new(SigningKeyBytes);

		Span<byte> passwordBytes = Encoding.ASCII.GetBytes(unhashed);
		Span<byte> passwordHash = hmac.ComputeHash(passwordBytes.ToArray());
		return Convert.ToBase64String(passwordHash);
	}

	public JwtSecurityToken GenerateFrom(User user) {

		List<Claim> claims =
		[
			// new Claim(JwtRegisteredClaimNames.Name, user.Username),
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(RoleClaim, user.Auth.GetRoles()),

			new Claim(JwtRegisteredClaimNames.Iss, Issuer),
			new Claim(JwtRegisteredClaimNames.Aud, Audience),
		];

		SigningCredentials cred = new(_securityKey, SecurityAlgorithms.HmacSha512Signature);
		return new(
			claims: claims,
			expires: DateTime.UtcNow.AddSeconds(ExpirationSeconds),
			signingCredentials: cred
		);
	}
}

public static class JwtUtils {
	public static string Write(this JwtSecurityToken token) {
		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}