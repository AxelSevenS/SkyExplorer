namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("lessons")]
public record Lesson : IEntity<Lesson, LessonCreateDTO, LessonUpdateDTO> {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }

	[Column("flight_id")]
	[JsonPropertyName("flightId")]
	public uint FlightId { get; set; }

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
	public Lesson(LessonCreateDTO dto) : this() {
		Goals = dto.Goals;
		AchievedGoals = dto.AchievedGoals ?? string.Empty;
		Note = dto.Note ?? string.Empty;
	}


	public static Lesson CreateFrom(LessonCreateDTO dto) => new(dto);
	public void Update(LessonUpdateDTO dto) {
		if (dto.Goals is not null) Goals = dto.Goals;
		if (dto.AchievedGoals is not null) AchievedGoals = dto.AchievedGoals;
		if (dto.Note is not null) Note = dto.Note;
	}
}

[Serializable]
public record LessonCreateDTO {
	[JsonPropertyName("flightId")]
	public uint FlightId { get; set; }

	[JsonPropertyName("goals")]
	public string? Goals { get; set; }

	[JsonPropertyName("achievedGoals")]
	public string? AchievedGoals { get; set; }

	[JsonPropertyName("note")]
	public string? Note { get; set; }
}

[Serializable]
public record LessonUpdateDTO {

	[JsonPropertyName("goals")]
	public string? Goals { get; set; }

	[JsonPropertyName("achievedGoals")]
	public string? AchievedGoals { get; set; }

	[JsonPropertyName("note")]
	public string? Note { get; set; }
}