using System.Text.Json;
using System.Text.Json.Serialization;

namespace SkyExplorer;

public class UserRolesJsonConverter : JsonConverter<AppUser.Roles> {
	public override AppUser.Roles Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
		string? readerString = reader.GetString();
		return readerString is null || ! Enum.TryParse(readerString, true, out AppUser.Roles role) ? AppUser.Roles.User : role;
	}

	public override void Write(Utf8JsonWriter writer, AppUser.Roles role, JsonSerializerOptions options) =>
		writer.WriteStringValue(role.ToString());
}