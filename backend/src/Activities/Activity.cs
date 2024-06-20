namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("activities")]
public record Activity : Entity<ActivityDTO> {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }

	[Column("flight_id")]
	[JsonPropertyName("flightId")]
	public uint FlightId { get; set; }

	[Column("title")]
	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;


	public Activity() : base() { }
	public Activity(ActivityDTO dto) : base(dto) { }


	public override void Update(ActivityDTO dto) {
		if (dto.FlightId.HasValue) FlightId = dto.FlightId.Value;
		if (dto.Title is not null) Title = dto.Title;
	}
}

public class ActivityDTO {
	[JsonPropertyName("flightId")]
	public uint? FlightId { get; set; }

	[JsonPropertyName("title")]
	public string? Title { get; set; }
}