namespace SkyExplorer;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("bills")]
public record Bill : IEntity {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }


	[Column("user_id")]
	[JsonIgnore]
	public uint UserId { get; set; }

	[ForeignKey(nameof(UserId))]
	[JsonPropertyName("user")]
	public AppUser User { get; set; }


	[Column]
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[Column("url")]
	[JsonPropertyName("url")]
	public string URL { get; set; }

	[Column("was_acquitted")]
	[JsonPropertyName("wasAcquitted")]
	public bool WasAcquitted { get; set; }

	[Column("created_at")]
	[JsonPropertyName("createdAt")]
	public DateTime CreatedAt { get; set; }


	public Bill() : base() { }
	public Bill(AppUser user, string url, string name, bool wasAcquitted) : this() {
		User = user;
		URL = url;
		Name = name;
		WasAcquitted = wasAcquitted;
		CreatedAt = DateTime.UtcNow;
	}
}

[Serializable]
public record BillSetupDto : IEntitySetup<Bill> {
	[JsonPropertyName("userId")]
	public uint UserId { get; set; }

	[JsonPropertyName("url")]
	public string URL { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("wasAcquitted")]
	public bool WasAcquitted { get; set; }


	public Bill? Create(AppDbContext context, out string error) {
		AppUser? user = context.Users.Find(UserId);
		if (user is null) {
			error = "Invalid User Id";
			return null;
		}

		error = string.Empty;
		return new(user, URL, Name, WasAcquitted);
	}
}

[Serializable]
public record BillUpdateDto : IEntityUpdate<Bill> {
	[JsonPropertyName("userId")]
	public uint? UserId { get; set; }

	[JsonPropertyName("url")]
	public string? URL { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("wasAcquitted")]
	public bool? WasAcquitted { get; set; }


	public bool TryUpdate(Bill entity, AppDbContext context, out string error) {
		if (UserId is not null) {
			if (context.Users.Find(UserId) is not AppUser user) {
				error = "Invalid User Id";
				return false;
			}
			entity.User = user;
		}

		if (URL is not null) entity.URL = URL;
		if (WasAcquitted is not null) entity.WasAcquitted = WasAcquitted.Value;
		if (Name is not null) entity.Name = Name;

		error = string.Empty;
		return true;
	}
}