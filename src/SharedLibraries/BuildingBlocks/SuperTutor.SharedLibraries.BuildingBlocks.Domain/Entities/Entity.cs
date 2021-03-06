using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Exceptions;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities;

public abstract class Entity<TIdentifier, TIdentifierValue> : IEquatable<Entity<TIdentifier, TIdentifierValue>>
    where TIdentifier : Identifier<TIdentifierValue>
    where TIdentifierValue : struct
{
    protected Entity(TIdentifier id) => Id = id;

    public TIdentifier Id { get; protected set; }

    protected void CheckInvariant(Invariant invariant)
    {
        if (invariant.IsBroken())
        {
            throw new InvariantValidationException(invariant);
        }
    }

    public bool Equals(Entity<TIdentifier, TIdentifierValue>? otherEntity) => otherEntity is not null && EntityEquals(otherEntity);

    public sealed override bool Equals(object? otherObject) => otherObject is Entity<TIdentifier, TIdentifierValue> otherEntity && EntityEquals(otherEntity);

    public sealed override int GetHashCode() => $"{GetType()}{Id.Value}}}".GetHashCode();

    private bool EntityEquals(Entity<TIdentifier, TIdentifierValue> otherEntity)
    {
        if (ReferenceEquals(this, otherEntity))
        {
            return true;
        }

        return GetType() == otherEntity.GetType() && Id == otherEntity.Id;
    }
}
