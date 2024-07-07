namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("courses")]
public record Course : IEntity {
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


	[Column("goals")]
	[JsonPropertyName("goals")]
	public string Goals { get; set; }

	[Column("achieved_goals")]
	[JsonPropertyName("achievedGoals")]
	public string AchievedGoals { get; set; }


	[Column("notes")]
	[JsonPropertyName("notes")]
	public string Notes { get; set; }


	public Course() : base() { }
	public Course(Flight flight, string goals, string achievedGoals, string notes) : this() {
		Flight = flight;
		Goals = goals;
		AchievedGoals = achievedGoals;
		Notes = notes;
	}
}

[Serializable]
public record CourseSetupDTO : IEntitySetup<Course> {
	[JsonPropertyName("flightId")]
	public uint FlightId { get; set; }

	[JsonPropertyName("goals")]
	public string? Goals { get; set; }

	[JsonPropertyName("achievedGoals")]
	public string? AchievedGoals { get; set; }

	[JsonPropertyName("notes")]
	public string? Notes { get; set; }

	public Course? Create(AppDbContext repo, out string error) {
		Flight? flight = repo.Flights.Include(f => f.Plane).FirstOrDefault(f => f.Id == FlightId);
		if (flight is null) {
			error = "Invalid Flight Id";
			return null;
		}

		error = string.Empty;
		return new(flight, Goals ?? string.Empty, AchievedGoals ?? string.Empty, Notes ?? string.Empty);
	}
}

[Serializable]
public record CourseUpdateDTO : IEntityUpdate<Course> {
	[JsonPropertyName("flightId")]
	public uint? FlightId { get; set; }

	[JsonPropertyName("goals")]
	public string? Goals { get; set; }

	[JsonPropertyName("achievedGoals")]
	public string? AchievedGoals { get; set; }

	[JsonPropertyName("notes")]
	public string? Notes { get; set; }


	public bool TryUpdate(Course entity, AppDbContext context, out string error) {
		if (FlightId is not null) entity.FlightId = FlightId.Value;
		if (Goals is not null) entity.Goals = Goals;
		if (AchievedGoals is not null) entity.AchievedGoals = AchievedGoals;
		if (Notes is not null) entity.Notes = Notes;

		error = string.Empty;
		return true;
	}
}