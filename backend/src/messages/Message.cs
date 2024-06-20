namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("messages")]
public record Message : Entity<MessageDTO> {
	[Key]
	[Column("id")]
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[Column("title")]
	[JsonPropertyName("title")]
	public string Title { get; set; }

	[Column("body")]
	[JsonPropertyName("body")]
	public string Body { get; set; }

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
	public Message(MessageDTO dto) : base(dto) { }


	public override void Update(MessageDTO dto) {
		if (dto.Title is not null) Title = dto.Title;
		if (dto.Body is not null) Body = dto.Body;
		if (dto.SendingDate.HasValue) SendingDate = dto.SendingDate.Value;
		if (dto.SenderId.HasValue) SenderId = dto.SenderId.Value;
		if (dto.RecipientId.HasValue) RecipientId = dto.RecipientId.Value;
	}
}

public record MessageDTO {
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("body")]
	public string? Body { get; set; }

	[JsonPropertyName("sendingDate")]
	public DateTime? SendingDate { get; set; }

	[JsonPropertyName("senderId")]
	public int? SenderId { get; set; }

	[JsonPropertyName("recipientId")]
	public int? RecipientId { get; set; }
}