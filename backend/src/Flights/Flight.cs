namespace SkyExplorer;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("flights")]
public record Flight : IEntity {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }


	[Column("user_id")]
	[JsonPropertyName("userId")]
	public uint UserId { get; set; }

	[ForeignKey(nameof(UserId))]
	[JsonIgnore]
	public AppUser User { get; set; }


	[Column("overseer_id")]
	[JsonPropertyName("overseerId")]
	public uint OverseerId { get; set; }

	[ForeignKey(nameof(OverseerId))]
	[JsonIgnore]
	public AppUser Overseer { get; set; }


	[Column("bill_id")]
	[JsonPropertyName("billId")]
	public uint BillId { get; set; }

	[ForeignKey(nameof(BillId))]
	[JsonIgnore]
	public Bill Bill { get; set; }


	[Column("plane_id")]
	[JsonIgnore]
	public uint PlaneId { get; set; }

	[ForeignKey(nameof(PlaneId))]
	[JsonPropertyName("plane")]
	public Plane Plane { get; set; }


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
	public Flight(AppUser user, AppUser overseer, Bill bill, Plane plane, string flightType, TimeSpan duration, DateTime dateTime) : this() {
		User = user;
		Overseer = overseer;
		Bill = bill;
		Plane = plane;
		FlightType = flightType;
		Duration = duration;
		DateTime = dateTime;
	}
}


[Serializable]
public class FlightSetupDTO : IEntitySetup<Flight> {
	[JsonPropertyName("userId")]
	public uint UserId { get; set; }

	[JsonPropertyName("overseerId")]
	public uint OverseerId { get; set; }

	[JsonPropertyName("billId")]
	public uint BillId { get; set; }

	[JsonPropertyName("planeId")]
	public uint PlaneId { get; set; }

	[JsonPropertyName("flightType")]
	public string FlightType { get; set; }

	[JsonPropertyName("duration")]
	public TimeSpan Duration { get; set; }

	[JsonPropertyName("dateTime")]
	public DateTime DateTime { get; set; }


	public Flight? Create(AppDbContext repo, out string error) {
		AppUser? user = repo.Users.Find(UserId);
		if (user is null) {
			error = "Invalid User Id";
			return null;
		}
		AppUser? overseer = repo.Users.Find(OverseerId);
		if (overseer is null) {
			error = "Invalid Overseer Id";
			return null;
		}
		Bill? bill = repo.Bills.Find(BillId);
		if (bill is null) {
			error = "Invalid Bill Id";
			return null;
		}
		Plane? plane = repo.Planes.Find(PlaneId);
		if (plane is null) {
			error = "Invalid Plane Id";
			return null;
		}

		error = string.Empty;
		return new(user, overseer, bill, plane, FlightType, Duration, DateTime);
	}
}

[Serializable]
public class FlightUpdateDTO : IEntityUpdate<Flight> {

	[JsonPropertyName("overseerId")]
	public uint? OverseerId { get; set; }

	[JsonPropertyName("billId")]
	public uint? BillId { get; set; }

	[JsonPropertyName("planeId")]
	public uint? PlaneId { get; set; }

	[JsonPropertyName("duration")]
	public TimeSpan? Duration { get; set; }

	[JsonPropertyName("dateTime")]
	public DateTime? DateTime { get; set; }


	public bool TryUpdate(Flight entity, AppDbContext repo, out string error) {
		if (OverseerId is not null) {
			AppUser? overseer = repo.Users.Find(OverseerId);
			if (overseer is null) {
				error = "Invalid Overseer Id";
				return false;
			}
			entity.Overseer = overseer;
		}

		if (BillId is not null) {
			Bill? bill = repo.Bills.Find(BillId);
			if (bill is null) {
				error = "Invalid Bill Id";
				return false;
			}
			entity.Bill = bill;
		}

		if (PlaneId is not null) {
			Plane? plane = repo.Planes.Find(PlaneId);
			if (plane is null) {
				error = "Invalid Plane Id";
				return false;
			}
			entity.Plane = plane;
		}

		if (DateTime is not null) entity.DateTime = DateTime.Value;
		if (Duration is not null) entity.Duration = Duration.Value;

		error = string.Empty;
		return true;
	}
}