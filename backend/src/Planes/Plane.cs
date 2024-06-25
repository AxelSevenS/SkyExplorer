namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("planes")]
public record Plane : Entity<PlaneCreateDTO, PlaneUpdateDTO> {
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
	public string Status { get; set; }


	public Plane() { }
	public Plane(PlaneCreateDTO dto) : base(dto) {
		Name = dto.Name;
		Type = dto.Type;
		Status = dto.Status;
	}


	public override void Update(PlaneUpdateDTO dto) {
		if (dto.Status is not null) Status = dto.Status;
	}
}

[Serializable]
public record PlaneCreateDTO {
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("type")]
	public string Type { get; set; }

	[JsonPropertyName("status")]
	public string Status { get; set; }
}

[Serializable]
public record PlaneUpdateDTO {
	[JsonPropertyName("status")]
	public string Status { get; set; }
}