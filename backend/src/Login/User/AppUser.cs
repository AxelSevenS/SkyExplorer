namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

[Table("users")]
[Index(nameof(Email), IsUnique = true)]
public record AppUser {

	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }

	[Required]
	[Column("email")]
	[JsonPropertyName("email")]
	public string Email {
		get => email;
		set => email = value.ToLowerInvariant();
	}
	private string email = string.Empty;

	[Required]
	[Column("password")]
	[JsonIgnore]
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


	public AppUser() : base() { }
	public AppUser(UserRegisterDTO dto) : this() {
		Email = dto.Email;
		Password = dto.Password;
		FirstName = dto.FirstName ?? string.Empty;
		LastName = dto.LastName ?? string.Empty;
		Role = Roles.User;
	}



	[Flags]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum Roles : ushort {
		User,
		Collaborator,
		Staff,
		Admin,
	};
}


[Serializable]
public record UserRegisterDTO : IEntitySetup<AppUser> {
	[JsonPropertyName("email")]
	public string Email { get; set; }

	[JsonPropertyName("password")]
	public string Password { get; set; }

	[JsonPropertyName("firstName")]
	public string? FirstName { get; set; }

	[JsonPropertyName("lastName")]
	public string? LastName { get; set; }

	public AppUser? Create(AppDbContext context, out string error) {
		error = string.Empty;
		return new(this);
	}
}

[Serializable]
public record UserUpdateDTO : IEntityUpdate<AppUser> {
	[JsonPropertyName("email")]
	public string? Email { get; set; }

	[JsonPropertyName("password")]
	public string? Password { get; set; }

	[JsonPropertyName("firstName")]
	public string? FirstName { get; set; }

	[JsonPropertyName("lastName")]
	public string? LastName { get; set; }

	[JsonPropertyName("role")]
	public AppUser.Roles? Role { get; set; }

	public bool TryUpdate(AppUser entity, AppDbContext context, out string error) {
		if (Email is not null) entity.Email = Email;
		if (Password is not null) entity.Password = Password;
		if (FirstName is not null) entity.FirstName = FirstName;
		if (LastName is not null) entity.LastName = LastName;
		if (Role is not null) entity.Role = Role.Value;

		error = string.Empty;
		return true;
	}
}

[Serializable]
public record UserLoginDTO {
	[JsonPropertyName("email")]
	public string Email { get; set; }

	[JsonPropertyName("password")]
	public string Password { get; set; }
}