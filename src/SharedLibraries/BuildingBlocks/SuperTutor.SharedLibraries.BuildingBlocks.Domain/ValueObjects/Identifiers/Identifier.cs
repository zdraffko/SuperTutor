namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

public abstract class Identifier<TValue> : ValueObject
    where TValue : struct
{
    protected Identifier(TValue value)
    {
        Value = value;
    }

    public TValue Value { get; }

    protected sealed override IEnumerable<object> GetEqualityMembers()
    {
        yield return Value;
    }
}
