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
	[Column("first_name")]
	[JsonPropertyName("firstName")]
	public string FirstName { get; set; } = string.Empty;

	[Required]
	[Column("last_name")]
	[JsonPropertyName("lastName")]
	public string LastName { get; set; } = string.Empty;

	[Required]
	[Column("role")]
	[JsonPropertyName("role")]
	public Roles Role { get; set; } = Roles.User;


	public User() : base() { }
	public User(UserRegisterDTO dto) : this() {
		Email = dto.Email;
		Password = dto.Password;
		FirstName = dto.FirstName ?? string.Empty;
		LastName = dto.LastName ?? string.Empty;
		Role = Roles.User;
	}


	public static User CreateFrom(UserRegisterDTO dto) => new(dto);
	public void Update(UserUpdateDTO dto) {
		if (dto.Email is not null) Email = dto.Email;
		if (dto.Password is not null) Password = dto.Password;
		if (dto.FirstName is not null) FirstName = dto.FirstName;
		if (dto.LastName is not null) LastName = dto.LastName;
		if (dto.Role is not null) Role = dto.Role.Value;
	}



	[Flags]
	[JsonConverter(typeof(UserRolesJsonConverter))]
	public enum Roles : ushort {
		User,
		Collaborator,
		Staff,
		Admin,
	};
}


[Serializable]
public record UserUpdateDTO {
	[JsonPropertyName("email")]
	public string? Email { get; set; }

	[JsonPropertyName("password")]
	public string? Password { get; set; }

	[JsonPropertyName("firstName")]
	public string? FirstName { get; set; }

	[JsonPropertyName("lastName")]
	public string? LastName { get; set; }

	[JsonPropertyName("role")]
	public User.Roles? Role { get; set; }
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

	[JsonPropertyName("firstName")]
	public string? FirstName { get; set; }

	[JsonPropertyName("lastName")]
	public string? LastName { get; set; }
}