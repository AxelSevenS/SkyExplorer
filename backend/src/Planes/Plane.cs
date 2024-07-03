namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("planes")]
public record Plane : IEntity<Plane, PlaneCreateDTO, PlaneUpdateDTO> {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public int Id { get; set; }

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
	public Plane(PlaneCreateDTO dto) : this() {
		Name = dto.Name;
		Type = dto.Type;
		Status = dto.Status;
	}


	public static Plane CreateFrom(PlaneCreateDTO dto) => new(dto);
	public void Update(PlaneUpdateDTO dto) {
		if (dto.Status.HasValue) Status = dto.Status.Value;
	}



	public enum Availability {
		Available,
		Maintenance,
		Unavailable
	}
}

[Serializable]
public record PlaneCreateDTO {
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("type")]
	public string Type { get; set; }

	[JsonPropertyName("status")]
	public Plane.Availability Status { get; set; }
}

[Serializable]
public record PlaneUpdateDTO {
	[JsonPropertyName("status")]
	public Plane.Availability? Status { get; set; }
}