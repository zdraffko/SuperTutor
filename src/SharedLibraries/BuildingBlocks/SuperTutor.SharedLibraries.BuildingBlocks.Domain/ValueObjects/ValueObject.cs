using System.Reflection;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public bool Equals(ValueObject? otherValueObject) => otherValueObject is not null && ValueObjectEquals(otherValueObject);

    public sealed override bool Equals(object? otherObject) => otherObject is ValueObject otherValueObject && ValueObjectEquals(otherValueObject);

    public sealed override int GetHashCode() => GetEqualityMembers().Aggregate(0, (a, b) => (a * 97) + b.GetHashCode());

    protected virtual IEnumerable<object> GetEqualityMembers()
    {
        foreach (var propertyInfo in GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
        {
            var propertyValue = propertyInfo.GetValue(this);
            if (propertyValue is not null)
            {
                yield return propertyValue;
            }
        }
    }

    private bool ValueObjectEquals(ValueObject otherValueObject)
    {
        if (ReferenceEquals(this, otherValueObject))
        {
            return true;
        }

        return GetType() == otherValueObject.GetType() && GetEqualityMembers().SequenceEqual(otherValueObject.GetEqualityMembers());
    }

    public static bool operator ==(ValueObject firstValueObject, ValueObject secondValueObject) => firstValueObject.ValueObjectEquals(secondValueObject);

    public static bool operator !=(ValueObject firstValueObject, ValueObject secondValueObject) => !(firstValueObject == secondValueObject);
}
