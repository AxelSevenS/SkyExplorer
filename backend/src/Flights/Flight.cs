namespace SkyExplorer;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("flights")]
public record Flight : Entity<FlightDTO> {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }

	[Column("user_id")]
	[JsonPropertyName("userId")]
	public uint UserId { get; set; }

	[Column("overseer_id")]
	[JsonPropertyName("overseerId")]
	public uint OverseerId { get; set; }

	[Column("bill_id")]
	[JsonPropertyName("billId")]
	public uint BillId { get; set; }

	[Column("plane_id")]
	[JsonPropertyName("planeId")]
	public uint PlaneId { get; set; }

	[Column("flight_type")]
	[JsonPropertyName("flightType")]
	public string FlightType { get; set; }

	[Column("duration")]
	[JsonPropertyName("duration")]
	public TimeSpan Duration { get; set; }

	[Column("date_time")]
	[JsonPropertyName("dateTime")]
	public DateTime DateTime { get; set; }


	public Flight() : base() { }
	public Flight(FlightDTO dto) : base(dto) { }


	public override void Update(FlightDTO dto) {
		if (dto.UserId.HasValue) UserId = dto.UserId.Value;
		if (dto.OverseerId.HasValue) OverseerId = dto.OverseerId.Value;
		if (dto.BillId.HasValue) BillId = dto.BillId.Value;
		if (dto.PlaneId.HasValue) PlaneId = dto.PlaneId.Value;
		if (dto.Duration.HasValue) Duration = dto.Duration.Value;
		if (dto.FlightType is not null) FlightType = dto.FlightType;
	}
}

public class FlightDTO {
	[JsonPropertyName("userId")]
	public uint? UserId { get; set; }

	[JsonPropertyName("overseerId")]
	public uint? OverseerId { get; set; }

	[JsonPropertyName("billId")]
	public uint? BillId { get; set; }

	[JsonPropertyName("planeId")]
	public uint? PlaneId { get; set; }

	[JsonPropertyName("flightType")]
	public string? FlightType { get; set; }

	[JsonPropertyName("duration")]
	public TimeSpan? Duration { get; set; }

	[JsonPropertyName("dateTime")]
	public DateTime? DateTime { get; set; }
}