using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.Common;

internal class IdentifierTypeHelper
{
    private static readonly ConcurrentDictionary<Type, Delegate> FactoriesCache = new();

    public static Func<TIdentifierValue, object>? GetFactory<TIdentifierValue>(Type identifierType) where TIdentifierValue : struct
        => FactoriesCache.GetOrAdd(identifierType, CreateFactory<TIdentifierValue>) as Func<TIdentifierValue, object>;

    private static Func<TIdentifierValue, object> CreateFactory<TIdentifierValue>(Type typeToConvert) where TIdentifierValue : struct
    {
        if (!IsIdentifier(typeToConvert))
        {
            throw new ArgumentException($"Type '{typeToConvert}' is not an identifier type", nameof(typeToConvert));
        }

        var typeToConvertConstructor = typeToConvert.GetConstructor(new[] { typeof(TIdentifierValue) });
        if (typeToConvertConstructor is null)
        {
            throw new ArgumentException($"Type '{typeToConvert}' does not have a constructor with one parameter of type '{typeof(TIdentifierValue)}'", nameof(typeToConvert));
        }

        var valueParameterExpression = Expression.Parameter(typeof(TIdentifierValue), "value");
        var constructorCallExpression = Expression.New(typeToConvertConstructor, valueParameterExpression);
        var identifierFactoryExpression = Expression.Lambda<Func<TIdentifierValue, object>>(constructorCallExpression, valueParameterExpression);

        return identifierFactoryExpression.Compile();
    }

    public static bool IsIdentifier(Type type) => IsIdentifier(type, out _);

    public static bool IsIdentifier(Type typeToConvert, [NotNullWhen(true)] out Type? identifierValueType)
    {
        if (typeToConvert is null)
        {
            throw new ArgumentNullException(nameof(typeToConvert));
        }

        if (typeToConvert.BaseType is Type identifier && identifier.IsGenericType && identifier.GetGenericTypeDefinition() == typeof(Identifier<>))
        {
            identifierValueType = identifier.GetGenericArguments()[0];

            return true;
        }

        identifierValueType = null;

        return false;
    }
}
