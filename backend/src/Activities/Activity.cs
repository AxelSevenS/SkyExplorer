namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("activities")]
public record Activity : Entity<ActivityCreateDTO, ActivityUpdateDTO> {
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
	public Activity(ActivityCreateDTO dto) : base(dto) {
		FlightId = dto.FlightId ?? 0;
		Title = dto.Title ?? string.Empty;
	}


	public override void Update(ActivityUpdateDTO dto) {
		if (dto.Title is not null) Title = dto.Title;
	}
}

[Serializable]
public class ActivityCreateDTO {
	[JsonPropertyName("flightId")]
	public uint? FlightId { get; set; }

	[JsonPropertyName("title")]
	public string? Title { get; set; }
}

[Serializable]
public class ActivityUpdateDTO {
	[JsonPropertyName("title")]
	public string Title { get; set; }
}