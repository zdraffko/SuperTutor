using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.Common;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.TypeConverters;

internal class IdentifierTypeConverter : TypeConverter
{
    private static readonly ConcurrentDictionary<Type, TypeConverter> ConvertersCache = new();

    private readonly TypeConverter currentConverter;

    public IdentifierTypeConverter(Type typeToConvert)
    {
        currentConverter = ConvertersCache.GetOrAdd(typeToConvert, CreateConverter!);
    }

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) => currentConverter.CanConvertFrom(context, sourceType);

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) => currentConverter.CanConvertTo(context, destinationType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) => currentConverter.ConvertFrom(context, culture, value);

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType) => currentConverter.ConvertTo(context, culture, value, destinationType);

    private static TypeConverter? CreateConverter(Type typeToConvert)
    {
        if (!IdentifierTypeHelper.IsIdentifier(typeToConvert, out var identifierValueType))
        {
            throw new InvalidOperationException($"The type '{typeToConvert}' is not an identifier type");
        }

        var identifierTypeConverter = typeof(IdentifierTypeConverter<>).MakeGenericType(identifierValueType);

        return Activator.CreateInstance(identifierTypeConverter, typeToConvert) as TypeConverter;
    }
}

internal class IdentifierTypeConverter<TIdentifierValue> : TypeConverter
    where TIdentifierValue : struct
{
    private static readonly TypeConverter IdentifierValueConverter = GetIdentifierValueConverter();

    private readonly Type identifierType;

    public IdentifierTypeConverter(Type identifierType)
    {
        this.identifierType = identifierType;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string) || sourceType == typeof(TIdentifierValue) || base.CanConvertFrom(context, sourceType);

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        => destinationType == typeof(string) || destinationType == typeof(TIdentifierValue) || base.CanConvertTo(context, destinationType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string stringValue)
        {
            value = IdentifierValueConverter.ConvertFrom(stringValue)!;
        }

        if (value is TIdentifierValue idValue)
        {
            var identifierFactory = IdentifierTypeHelper.GetFactory<TIdentifierValue>(identifierType);
            if (identifierFactory is null)
            {
                return default;
            }

            return identifierFactory(idValue);
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value is not Identifier<TIdentifierValue> identifier)
        {
            return default;
        }

        var identifierValue = identifier.Value;

        if (destinationType == typeof(string))
        {
            return identifierValue.ToString();
        }

        if (destinationType == typeof(TIdentifierValue))
        {
            return identifierValue;
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }

    private static TypeConverter GetIdentifierValueConverter()
    {
        var identifierValueConverter = TypeDescriptor.GetConverter(typeof(TIdentifierValue));
        if (!identifierValueConverter.CanConvertFrom(typeof(string)))
        {
            throw new InvalidOperationException($"Identifier value type '{typeof(TIdentifierValue)}' doesn't have a converter that can convert from string");
        }

        return identifierValueConverter;
    }
}
