namespace SkyExplorer;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("messages")]
public record Message : IEntity<Message, MessageCreateDTO, MessageUpdateDTO> {
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
	public Message(MessageCreateDTO dto) : this() {
		Title = dto.Title;
		Body = dto.Body;
		SendingDate = dto.SendingDate;
		SenderId = dto.SenderId;
		RecipientId = dto.RecipientId;
	}


	public static Message CreateFrom(MessageCreateDTO dto) => new(dto);
	public void Update(MessageUpdateDTO dto) {
		if (dto.Title is not null) Title = dto.Title;
		if (dto.Body is not null) Body = dto.Body;
	}
}

[Serializable]
public record MessageCreateDTO {
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
}

[Serializable]
public record MessageUpdateDTO {
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("body")]
	public string? Body { get; set; }
}