namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("messages")]
public record Message : IEntity {
	[Key]
	[Column("id")]
	[JsonPropertyName("id")]
	public uint Id { get; set; }

	[Column("title")]
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[Column("body")]
	[JsonPropertyName("body")]
	public string? Body { get; set; }

	[Column("sending_date")]
	[JsonPropertyName("sendingDate")]
	public DateTime SendingDate { get; set; }

	[Column("sender_id")]
	[JsonIgnore]
	public uint SenderId { get; set; }

	[ForeignKey(nameof(SenderId))]
	[JsonPropertyName("sender")]
	public AppUser Sender { get; set; }


	[Column("recipient_id")]
	[JsonIgnore]
	public uint RecipientId { get; set; }

	[ForeignKey(nameof(RecipientId))]
	[JsonPropertyName("recipient")]
	public AppUser Recipient { get; set; }


	public Message() { }
	public Message(string title, string body, DateTime sendingDate, AppUser sender, AppUser recipient) : this() {
		Title = title;
		Body = body;
		SendingDate = sendingDate;
		Sender = sender;
		Recipient = recipient;
	}
}

[Serializable]
public record MessageSetupDTO : IEntitySetup<Message>{
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("body")]
	public string? Body { get; set; }

	[JsonPropertyName("sendingDate")]
	public DateTime SendingDate { get; set; }

	[JsonPropertyName("senderId")]
	public int SenderId { get; set; }

	[JsonPropertyName("recipientId")]
	public int RecipientId { get; set; }

	public Message? Create(AppDbContext context, out string error) {
		AppUser? sender = context.Users.Find(SenderId);
		if (sender is null) {
			error = "Invalid Sender Id";
			return null;
		}

		AppUser? recipient = context.Users.Find(RecipientId);
		if (recipient is null) {
			error = "Invalid Recipient Id";
			return null;
		}

		error = string.Empty;
		return new(Title ?? string.Empty, Body ?? string.Empty, SendingDate, sender, recipient);
	}
}

[Serializable]
public record MessageUpdateDTO : IEntityUpdate<Message> {
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("body")]
	public string? Body { get; set; }

	public bool TryUpdate(Message entity, AppDbContext context, out string error) {
		if (Title is not null) entity.Title = Title;
		if (Body is not null) entity.Body = Body;

		error = string.Empty;
		return true;
	}
}