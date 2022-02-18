using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.TypeConversion;
using System.ComponentModel;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

[TypeConverter(typeof(IdentifierTypeConverter))]
public abstract class Identifier<TValue> : ValueObject
    where TValue : struct
{
    protected Identifier(TValue value) => Value = value;

    public TValue Value { get; }

    protected sealed override IEnumerable<object> GetEqualityMembers()
    {
        yield return Value;
    }
}
