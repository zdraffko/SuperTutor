using System.Reflection;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

public abstract class Enumeration : IComparable
{
    public int Value { get; private set; }

    public string Name { get; private set; }

    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public static IEnumerable<TEnumeration> All<TEnumeration>() where TEnumeration : Enumeration 
        => typeof(TEnumeration)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(fieldInfo => fieldInfo.GetValue(null))
            .Cast<TEnumeration>();

    public static TEnumeration? FromValue<TEnumeration>(int value) where TEnumeration : Enumeration
        => All<TEnumeration>().FirstOrDefault(enumeration => enumeration.Value == value);

    public static IEnumerable<TEnumeration> FromValues<TEnumeration>(IEnumerable<int> values) where TEnumeration : Enumeration
    => All<TEnumeration>().Where(enumeration => values.Contains(enumeration.Value));

    public static TEnumeration? FromName<TEnumeration>(string name) where TEnumeration : Enumeration
        => All<TEnumeration>().FirstOrDefault(enumeration => enumeration.Name == name);

    public int CompareTo(object? otherObject) => Value.CompareTo(((Enumeration)otherObject!).Value);

    public override string ToString() => Name;

    public override int GetHashCode() => Value.GetHashCode();

    public override bool Equals(object? otherObject)
    {
        if (otherObject is not Enumeration otherEnumeration)
        {
            return false;
        }

        var doesTypeMatch = GetType().Equals(otherObject.GetType());
        var doesValueMatch = Value.Equals(otherEnumeration.Value);

        return doesTypeMatch && doesValueMatch;
    }
}
