using System.Text.Json;
using System.Text.Json.Serialization;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null)
        {
            return default;
        }

        if (!DateOnly.TryParseExact(reader.GetString(), "dd/MM/yyyy", out var result))
        {
            return default;
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        var rawDateOnly = value.ToString("dd/MM/yyyy");

        writer.WriteStringValue(rawDateOnly);
    }
}
