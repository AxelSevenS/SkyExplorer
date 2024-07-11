namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

[Table("planes")]
public record Plane : IEntity {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }

	[Column("name")]
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[Column("type")]
	[JsonPropertyName("type")]
	public string Type { get; set; }

	[Column("status")]
	[JsonPropertyName("status")]
	public Availability Status { get; set; }


	public Plane() { }
	public Plane(PlaneSetupDto dto) : this() {
		Name = dto.Name;
		Type = dto.Type;
		Status = dto.Status ?? Availability.Available;
	}


	[Serializable]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum Availability {
		Available,
		Maintenance,
		Unavailable
	}
}

[Serializable]
public record PlaneSetupDto : IEntitySetup<Plane> {
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("type")]
	public string Type { get; set; }

	[JsonPropertyName("status")]
	public Plane.Availability? Status { get; set; }

	public Plane? Create(AppDbContext context, out string error) {
		error = string.Empty;
		return new(this);
	}
}

[Serializable]
public record PlaneUpdateDto : IEntityUpdate<Plane> {
	[JsonPropertyName("status")]
	public Plane.Availability? Status { get; set; }

	public bool TryUpdate(Plane entity, AppDbContext context, out string error) {
		if (Status is not null) entity.Status = Status.Value;

		error = string.Empty;
		return true;
	}
}