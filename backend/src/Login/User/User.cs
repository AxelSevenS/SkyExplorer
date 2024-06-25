namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("users")]
public record User : Entity<UserDTO> {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }

	[Required]
	[Column("firstname")]
	[JsonPropertyName("firstname")]
	public string Firstname { get; set; } = string.Empty;

	[Required]
	[Column("lastname")]
	[JsonPropertyName("lastname")]
	public string Lastname { get; set; } = string.Empty;

	[Required]
	[Column("email")]
	[JsonPropertyName("email")]
	public string Email { get; set; } = string.Empty;

	[Required]
	[Column("password")]
	[JsonPropertyName("password")]
	public string Password { get; set; } = string.Empty;

	[Required]
	[Column("authorizations")]
	[JsonPropertyName("authorizations")]
	public Authorizations Auth { get; set; } = (Authorizations)Positions.User;


	public User() : base() { }
	public User(UserDTO dto) : base(dto) { }


	public override void Update(UserDTO dto) {
		if (dto.Firstname is not null) Firstname = dto.Firstname;
		if (dto.Lastname is not null) Lastname = dto.Lastname;
		if (dto.Email is not null) Email = dto.Email;
		if (dto.Password is not null) Password = dto.Password;
	}



	[Flags]
	public enum Positions : ushort {
		User = 0,

		Collaborator = User,

		Staff = Collaborator,

		Admin = ushort.MaxValue,
	}

	[Flags]
	[JsonConverter(typeof(UserAuthorizationsJsonConverter))]
	public enum Authorizations : ushort {
		EditAnyUser = 1 << 0,        // 0b0000_0000_0000_0001
		EditUserAuths = 1 << 1,      // 0b0000_0000_0000_0010
		DeleteAnyUser = 1 << 2,      // 0b0000_0000_0000_0100
	}
}



public record UserDTO {
	[JsonPropertyName("firstname")]
	public string? Firstname { get; set; }

	[JsonPropertyName("lastname")]
	public string? Lastname { get; set; }

	[JsonPropertyName("email")]
	public string? Email { get; set; }

	[JsonPropertyName("password")]
	public string? Password { get; set; }

	[JsonPropertyName("roles")]
	public User.Authorizations? Auth { get; set; }
}