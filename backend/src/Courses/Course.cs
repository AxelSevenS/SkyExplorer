namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("courses")]
[Index(nameof(FlightId), IsUnique = true)]
public record Course : IEntity {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }


	[Column("name")]
	[JsonPropertyName("name")]
	public string Name { get; set; }


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

	[Column("acquired_skills")]
	[JsonPropertyName("acquiredSkills")]
	public string AcquiredSkills { get; set; } = string.Empty;

	public Course() : base() { }
	public Course(string name, Flight flight, string goals, string achievedGoals, string notes) : this() {
		Name = name;
		Flight = flight;
		Goals = goals;
		AchievedGoals = achievedGoals;
		Notes = notes;
	}
}

[Serializable]
public record CourseSetupDto : IEntitySetup<Course> {
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("flightId")]
	public uint FlightId { get; set; }

	[JsonPropertyName("goals")]
	public string? Goals { get; set; }

	[JsonPropertyName("achievedGoals")]
	public string? AchievedGoals { get; set; }

	[JsonPropertyName("notes")]
	public string? Notes { get; set; }

	public Course? Create(AppDbContext context, out string error) {
		Flight? flight = context.Flights
			.Include(f => f.User)
			.Include(f => f.Overseer)
			.Include(f => f.Plane)
			.FirstOrDefault(f => f.Id == FlightId);

		if (flight is null) {
			error = "Invalid Flight Id";
			return null;
		}

		error = string.Empty;
		return new(Name, flight, Goals ?? string.Empty, AchievedGoals ?? string.Empty, Notes ?? string.Empty);
	}
}

[Serializable]
public record CourseUpdateDto : IEntityUpdate<Course> {
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("flightId")]
	public uint? FlightId { get; set; }

	[JsonPropertyName("goals")]
	public string? Goals { get; set; }

	[JsonPropertyName("achievedGoals")]
	public string? AchievedGoals { get; set; }

	[JsonPropertyName("notes")]
	public string? Notes { get; set; }

	[JsonPropertyName("acquiredSkills")]
	public string? AcquiredSkills { get; set; }

	public bool TryUpdate(Course entity, AppDbContext context, out string error) {
		if (FlightId is not null) {
			Flight? flight = context.Flights.Find(FlightId);
			if (flight is null) {
				error = "Invalid Flight Id";
				return false;
			}
			entity.Flight = flight;
		}

		if (Name is not null) entity.Name = Name;

		if (Goals is not null) entity.Goals = Goals;
		if (AchievedGoals is not null) entity.AchievedGoals = AchievedGoals;
		if (Notes is not null) entity.Notes = Notes;
		if (AcquiredSkills is not null) entity.AcquiredSkills = AcquiredSkills;

		error = string.Empty;
		return true;
	}
}