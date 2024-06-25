namespace SkyExplorer;

using System.Text.Json;
using System.Text.Json.Serialization;

public static class AuthorizationsExtensions {
	public static string FormatAuths(this User.Authorizations authorizations) =>
		authorizations.GetAuthStrings().FormatAuths();


	public static IEnumerable<string> GetAuthStrings(this User.Authorizations authorizations) {
		List<string> strings = [];

		foreach (User.Authorizations auth in Enum.GetValues<User.Authorizations>()) {
			if ((authorizations & auth) != 0) strings.Add(auth.ToString());
		}

		return strings;
	}
	public static string FormatAuths(this IEnumerable<string> authStrings) =>
		string.Join(',', authStrings);



	public static User.Authorizations ParseAuths(this string formatted) =>
		formatted.GetAuthStrings().ParseAuths();

	public static IEnumerable<string> GetAuthStrings(this string formatted) =>
		formatted.Split(',');

	public static User.Authorizations ParseAuths(this IEnumerable<string> strings) {
		User.Authorizations authorizations = 0;

		foreach (User.Authorizations auth in Enum.GetValues<User.Authorizations>()) {
			if (strings.Contains(auth.ToString())) authorizations |= auth;
		}

		return authorizations;
	}
}

public class UserAuthorizationsJsonConverter : JsonConverter<User.Authorizations> {
	public override User.Authorizations Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		reader.GetString()?.ParseAuths() ?? 0;

	public override void Write(Utf8JsonWriter writer, User.Authorizations auths, JsonSerializerOptions options) =>
		writer.WriteStringValue(auths.FormatAuths());
}