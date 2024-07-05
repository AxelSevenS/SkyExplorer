namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("lessons")]
public record Lesson {
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


	[Column("goals")]
	[JsonPropertyName("goals")]
	public string Goals { get; set; }

	[Column("achieved_goals")]
	[JsonPropertyName("achievedGoals")]
	public string AchievedGoals { get; set; }


	[Column("note")]
	[JsonPropertyName("note")]
	public string Note { get; set; }


	public Lesson() : base() { }
	public Lesson(LessonSetupDTO dto, AppDbContext context) : this() {
		Flight = context.Flights.Find(dto.FlightId)!;
		Goals = dto.Goals ?? string.Empty;
		AchievedGoals = dto.AchievedGoals ?? string.Empty;
		Note = dto.Note ?? string.Empty;
	}
}

[Serializable]
public record LessonSetupDTO : IEntitySetup<Lesson> {
	[JsonPropertyName("flightId")]
	public uint FlightId { get; set; }

	[JsonPropertyName("goals")]
	public string? Goals { get; set; }

	[JsonPropertyName("achievedGoals")]
	public string? AchievedGoals { get; set; }

	[JsonPropertyName("note")]
	public string? Note { get; set; }

	public Lesson? Create(AppDbContext context, out string error) {
		if (context.Flights.Find(FlightId) is null) {
			error = "Invalid Flight Id";
			return null;
		}

		error = string.Empty;
		return new(this, context);
	}
}

[Serializable]
public record LessonUpdateDTO : IEntityUpdate<Lesson> {

	[JsonPropertyName("goals")]
	public string? Goals { get; set; }

	[JsonPropertyName("achievedGoals")]
	public string? AchievedGoals { get; set; }

	[JsonPropertyName("note")]
	public string? Note { get; set; }


	public bool TryUpdate(Lesson entity, AppDbContext context, out string error) {
		if (Goals is not null) entity.Goals = Goals;
		if (AchievedGoals is not null) entity.AchievedGoals = AchievedGoals;
		if (Note is not null) entity.Note = Note;

		error = string.Empty;
		return true;
	}
}