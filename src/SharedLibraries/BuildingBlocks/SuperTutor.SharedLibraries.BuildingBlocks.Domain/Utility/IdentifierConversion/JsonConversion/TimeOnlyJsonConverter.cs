using System.Text.Json;
using System.Text.Json.Serialization;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null)
        {
            return default;
        }

        if (!TimeOnly.TryParseExact(reader.GetString(), "HH:mm:ss", out var result))
        {
            return default;
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        var rawTimeOnly = value.ToString("HH:mm:ss");

        writer.WriteStringValue(rawTimeOnly);
    }
}
