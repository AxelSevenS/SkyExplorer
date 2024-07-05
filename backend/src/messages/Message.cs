namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("messages")]
public record Message {
	[Key]
	[Column("id")]
	[JsonPropertyName("id")]
	public int Id { get; set; }

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
	[JsonPropertyName("senderId")]
	public int SenderId { get; set; }

	[Column("recipient_id")]
	[JsonPropertyName("recipientId")]
	public int RecipientId { get; set; }


	public Message() { }
	public Message(MessageSetupDTO dto) : this() {
		Title = dto.Title;
		Body = dto.Body;
		SendingDate = dto.SendingDate;
		SenderId = dto.SenderId;
		RecipientId = dto.RecipientId;
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
		if (context.Users.Find(SenderId) is null) {
			error = "Invalid Sender Id";
			return null;
		}

		if (context.Users.Find(RecipientId) is null) {
			error = "Invalid Recipient Id";
			return null;
		}

		error = string.Empty;
		return new(this);
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