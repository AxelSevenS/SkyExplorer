using System.Text.Json;
using System.Text.Json.Serialization;

namespace SkyExplorer;

public class UserRolesJsonConverter : JsonConverter<User.Roles> {
	public override User.Roles Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
		string? readerString = reader.GetString();
		return readerString is null || ! Enum.TryParse(readerString, true, out User.Roles role) ? User.Roles.User : role;
	}

	public override void Write(Utf8JsonWriter writer, User.Roles role, JsonSerializerOptions options) =>
		writer.WriteStringValue(role.ToString());
}