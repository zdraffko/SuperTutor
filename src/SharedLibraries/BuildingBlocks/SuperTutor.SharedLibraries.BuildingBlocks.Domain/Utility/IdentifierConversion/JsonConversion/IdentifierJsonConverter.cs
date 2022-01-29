using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.Common;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.JsonConversion.Identifiers;

internal class IdentifierJsonConverter<TIdentifier, TIdentifierValue> : JsonConverter<TIdentifier>
    where TIdentifier : Identifier<TIdentifierValue>
    where TIdentifierValue : struct
{
    public override TIdentifier? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null)
        {
            return default;
        }

        var identifierValue = JsonSerializer.Deserialize<TIdentifierValue>(ref reader, options);
        var identifierFactory = IdentifierTypeHelper.GetFactory<TIdentifierValue>(typeToConvert);
        if (identifierFactory is null)
        {
            return default;
        }

        return identifierFactory(identifierValue) as TIdentifier;
    }

    public override void Write(Utf8JsonWriter writer, TIdentifier identifier, JsonSerializerOptions options)
    {
        if (identifier is null)
        {
            writer.WriteNullValue();

            return;
        }
        
        JsonSerializer.Serialize(writer, identifier.Value, options);
    }
}
