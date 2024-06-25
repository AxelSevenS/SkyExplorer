namespace SkyExplorer;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("bills")]
public record Bill : Entity<BillCreateDTO, BillUpdateDTO> {
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
	public Bill(BillCreateDTO dto) : base(dto) {
		URL = dto.URL;
		WasAcquitted = dto.WasAcquitted;
		CreatedAt = DateTime.Now;
	}

	public override void Update(BillUpdateDTO dto) {
		if (dto.URL is not null) URL = dto.URL;
		if (dto.WasAcquitted is not null) WasAcquitted = dto.WasAcquitted.Value;
	}
}

[Serializable]
public class BillCreateDTO {
	[JsonPropertyName("url")]
	public string URL { get; set; }

	[JsonPropertyName("wasAcquitted")]
	public bool WasAcquitted { get; set; }

	[JsonPropertyName("createdAt")]
	public bool CreatedAt { get; set; }
}

[Serializable]
public class BillUpdateDTO {
	[JsonPropertyName("url")]
	public string? URL { get; set; }

	[JsonPropertyName("wasAcquitted")]
	public bool? WasAcquitted { get; set; }
}