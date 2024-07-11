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
	[JsonIgnore]
	public uint UserId { get; set; }

	[ForeignKey(nameof(UserId))]
	[JsonPropertyName("user")]
	public AppUser User { get; set; }


	[Column("overseer_id")]
	[JsonIgnore]
	public uint OverseerId { get; set; }

	[ForeignKey(nameof(OverseerId))]
	[JsonPropertyName("overseer")]
	public AppUser Overseer { get; set; }


	[Column("bill_id")]
	[JsonIgnore]
	public uint BillId { get; set; }

	[ForeignKey(nameof(BillId))]
	[JsonPropertyName("bill")]
	public Bill Bill { get; set; }


	[Column("plane_id")]
	[JsonIgnore]
	public uint PlaneId { get; set; }

	[ForeignKey(nameof(PlaneId))]
	[JsonPropertyName("plane")]
	public Plane Plane { get; set; }


	[Column("duration")]
	[JsonPropertyName("duration")]
	public TimeSpan Duration { get; set; }

	[Column("date_time")]
	[JsonPropertyName("dateTime")]
	public DateTime DateTime { get; set; }


	public Flight() : base() { }
	public Flight(AppUser user, AppUser overseer, Bill bill, Plane plane, TimeSpan duration, DateTime dateTime) : this() {
		User = user;
		Overseer = overseer;
		Bill = bill;
		Plane = plane;

		Duration = duration;
		DateTime = dateTime;
	}
}



[Flags]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FlightType : ushort {
	Lesson,
	Leasure
};

[Serializable]
public record FlightSetupDto : IEntitySetup<Flight> {
	[JsonPropertyName("userId")]
	public uint UserId { get; set; }

	[JsonPropertyName("overseerId")]
	public uint OverseerId { get; set; }

	[JsonPropertyName("billId")]
	public uint BillId { get; set; }

	[JsonPropertyName("planeId")]
	public uint PlaneId { get; set; }


	[JsonPropertyName("duration")]
	public TimeSpan Duration { get; set; }

	[JsonPropertyName("dateTime")]
	public DateTime DateTime { get; set; }


	public Flight? Create(AppDbContext context, out string error) {
		AppUser? user = context.Users.Find(UserId);
		if (user is null) {
			error = "Invalid User Id";
			return null;
		}
		AppUser? overseer = context.Users.Find(OverseerId);
		if (overseer is null) {
			error = "Invalid Overseer Id";
			return null;
		}
		Bill? bill = context.Bills.Find(BillId);
		if (bill is null) {
			error = "Invalid Bill Id";
			return null;
		}
		Plane? plane = context.Planes.Find(PlaneId);
		if (plane is null) {
			error = "Invalid Plane Id";
			return null;
		}

		error = string.Empty;
		return new(user, overseer, bill, plane, Duration, DateTime);
	}
}

[Serializable]
public record FlightUpdateDto : IEntityUpdate<Flight> {

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


	public bool TryUpdate(Flight entity, AppDbContext context, out string error) {
		if (OverseerId is not null) {
			if (context.Users.Find(OverseerId) is not AppUser overseer) {
				error = "Invalid Overseer Id";
				return false;
			}
			entity.Overseer = overseer;
		}

		if (BillId is not null) {
			if (context.Bills.Find(BillId) is not Bill bill) {
				error = "Invalid Bill Id";
				return false;
			}
			entity.Bill = bill;
		}

		if (PlaneId is not null) {
			if (context.Planes.Find(PlaneId) is not Plane plane) {
				error = "Invalid Plane Id";
				return false;
			}
			entity.Plane = plane;
		}

		if (Duration is not null) entity.Duration = Duration.Value;
		if (DateTime is not null) entity.DateTime = DateTime.Value;

		error = string.Empty;
		return true;
	}
}