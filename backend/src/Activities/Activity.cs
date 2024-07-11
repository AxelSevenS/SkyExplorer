namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("activities")]
public record Activity : IEntity {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }


	[Column("flight_id")]
	[JsonIgnore]
	public uint FlightId { get; set; }

	[ForeignKey(nameof(FlightId))]
	[JsonPropertyName("flight")]
	public Flight Flight { get; set; }


	[Column("title")]
	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;


	public Activity() : base() { }
	public Activity(Flight flight, string title) : this() {
		Flight = flight;
		Title = title;
	}
}

[Serializable]
public record ActivitySetupDto : IEntitySetup<Activity> {
	[JsonPropertyName("flightId")]
	public uint FlightId { get; set; }

	[JsonPropertyName("title")]
	public string? Title { get; set; }

	public Activity? Create(AppDbContext context, out string error) {
		Flight? flight = context.Flights
			.Include(f => f.User)
			.Include(f => f.Overseer)
			.Include(f => f.Plane)
			.First(f => f.Id == FlightId);

		if (flight is null) {
			error = "Invalid Flight Id";
			return null;
		}

		error = string.Empty;
		return new(flight, Title ?? string.Empty);
	}
}

[Serializable]
public record ActivityUpdateDto : IEntityUpdate<Activity> {
	[JsonPropertyName("title")]
	public string Title { get; set; }

	public bool TryUpdate(Activity entity, AppDbContext context, out string error) {
		if (Title is not null) entity.Title = Title;

		error = string.Empty;
		return true;
	}
}