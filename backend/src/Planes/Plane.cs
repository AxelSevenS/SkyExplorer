namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("planes")]
public record Plane : Entity<PlaneDTO> {
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
	public Plane(PlaneDTO dto) : base(dto) { }


	public override void Update(PlaneDTO dto) {
		if (dto.Name is not null) Name = dto.Name;
		if (dto.Type is not null) Type = dto.Type;
		if (dto.Status is not null) Status = dto.Status;
	}
}

public record PlaneDTO {
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("type")]
	public string? Type { get; set; }

	[JsonPropertyName("status")]
	public string? Status { get; set; }
}