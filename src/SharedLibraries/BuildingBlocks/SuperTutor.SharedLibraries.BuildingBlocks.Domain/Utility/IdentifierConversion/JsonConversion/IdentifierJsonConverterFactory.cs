using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.Common;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.JsonConversion.Identifiers;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;

public class IdentifierJsonConverterFactory : JsonConverterFactory
{
    private static readonly ConcurrentDictionary<Type, JsonConverter> ConvertersCache = new();

    public override bool CanConvert(Type typeToConvert) => IdentifierTypeHelper.IsIdentifier(typeToConvert);

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) => ConvertersCache.GetOrAdd(typeToConvert, CreateConverter!);

    private static JsonConverter? CreateConverter(Type typeToConvert)
    {
        if (!IdentifierTypeHelper.IsIdentifier(typeToConvert, out var valueType))
        {
            throw new InvalidOperationException($"Cannot create identifier JSON converter for '{typeToConvert}'");
        }

        var identifierJsonConverterType = typeof(IdentifierJsonConverter<,>).MakeGenericType(typeToConvert, valueType);
        var identifierJsonConverter = Activator.CreateInstance(identifierJsonConverterType);

        return identifierJsonConverter as JsonConverter;
    }
}
