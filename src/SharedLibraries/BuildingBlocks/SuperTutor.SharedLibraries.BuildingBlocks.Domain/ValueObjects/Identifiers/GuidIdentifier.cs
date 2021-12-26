namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

public abstract class GuidIdentifier : Identifier<Guid>
{
    protected GuidIdentifier(Guid value) : base(value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("A Guid Identifier cannot be an empty guid.");
        }
    }
}
