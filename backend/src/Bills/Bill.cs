namespace SkyExplorer;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("bills")]
public record Bill : Entity<BillDTO> {
	[Key]
	[Column("id")]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("id")]
	public uint Id { get; set; }

	[Column("url")]
	[JsonPropertyName("url")]
	public string URL { get; set; }

	[Column("was_acquitted")]
	[JsonPropertyName("wasAcquitted")]
	public bool WasAcquitted { get; set; }

	[Column("created_at")]
	[JsonPropertyName("createdAt")]
	public DateTime CreatedAt { get; set; }


	public Bill() : base() { }
	public Bill(BillDTO dto) : base(dto) { }


	public override void Update(BillDTO dto) {
		if (dto.URL is not null) URL = dto.URL;
		if (dto.WasAcquitted.HasValue) WasAcquitted = dto.WasAcquitted.Value;
		if (dto.CreatedAt.HasValue) CreatedAt = dto.CreatedAt.Value;
	}
}

public class BillDTO {
	[JsonPropertyName("url")]
	public string? URL { get; set; }

	[JsonPropertyName("wasAcquitted")]
	public bool? WasAcquitted { get; set; }

	[JsonPropertyName("createdAt")]
	public DateTime? CreatedAt { get; set; }
}