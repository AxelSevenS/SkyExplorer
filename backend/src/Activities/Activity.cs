namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("activities")]
public record Activity {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }


	[Column("flight_id")]
	[JsonPropertyName("flightId")]
	public uint FlightId { get; set; }

	[ForeignKey(nameof(FlightId))]
	[JsonIgnore]
	public Flight Flight { get; set; }


	[Column("title")]
	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;


	public Activity() : base() { }
	public Activity(ActivitySetupDTO dto) : this() {
		FlightId = dto.FlightId;
		Title = dto.Title ?? string.Empty;
	}
}

[Serializable]
public class ActivitySetupDTO : IEntitySetup<Activity> {
	[JsonPropertyName("flightId")]
	public uint FlightId { get; set; }

	[JsonPropertyName("title")]
	public string? Title { get; set; }

	public Activity? Create(AppDbContext context, out string error) {
		if (context.Flights.Find(FlightId) is null) {
			error = "Invalid Flight Id";
			return null;
		}

		error = string.Empty;
		return new(this);
	}
}

[Serializable]
public class ActivityUpdateDTO : IEntityUpdate<Activity> {
	[JsonPropertyName("title")]
	public string Title { get; set; }

	public bool TryUpdate(Activity entity, AppDbContext context, out string error) {
		if (Title is not null) entity.Title = Title;

		error = string.Empty;
		return true;
	}
}