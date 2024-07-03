namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("users")]
public record User : IEntity<User, UserRegisterDTO, UserUpdateDTO> {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }

	[Required]
	[Column("email")]
	[JsonPropertyName("email")]
	public string Email { get; set; } = string.Empty;

	[Required]
	[Column("password")]
	[JsonPropertyName("password")]
	public string Password { get; set; } = string.Empty;

	[Required]
	[Column("firstname")]
	[JsonPropertyName("firstname")]
	public string FirstName { get; set; } = string.Empty;

	[Required]
	[Column("lastname")]
	[JsonPropertyName("lastname")]
	public string LastName { get; set; } = string.Empty;

	[Required]
	[Column("authorizations")]
	[JsonPropertyName("authorizations")]
	public Authorizations Auth { get; set; } = (Authorizations)Positions.User;


	public User() : base() { }
	public User(UserRegisterDTO dto) : this() {
		Email = dto.Email;
		Password = dto.Password;
		FirstName = dto.FirstName ?? string.Empty;
		LastName = dto.LastName ?? string.Empty;
		Auth = (Authorizations)Positions.User;
	}


	public static User CreateFrom(UserRegisterDTO dto) => new(dto);
	public void Update(UserUpdateDTO dto) {
		if (dto.Email is not null) Email = dto.Email;
		if (dto.Password is not null) Password = dto.Password;
		if (dto.FirstName is not null) FirstName = dto.FirstName;
		if (dto.LastName is not null) LastName = dto.LastName;
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

[Serializable]
public record UserUpdateDTO {
	[JsonPropertyName("email")]
	public string? Email { get; set; }

	[JsonPropertyName("password")]
	public string? Password { get; set; }

	[JsonPropertyName("firstname")]
	public string? FirstName { get; set; }

	[JsonPropertyName("lastname")]
	public string? LastName { get; set; }
}
[Serializable]
public record UserLoginDTO {
	[JsonPropertyName("email")]
	public string Email { get; set; }

	[JsonPropertyName("password")]
	public string Password { get; set; }
}

[Serializable]
public record UserRegisterDTO {
	[JsonPropertyName("email")]
	public string Email { get; set; }

	[JsonPropertyName("password")]
	public string Password { get; set; }

	[JsonPropertyName("firstname")]
	public string? FirstName { get; set; }

	[JsonPropertyName("lastname")]
	public string? LastName { get; set; }
}