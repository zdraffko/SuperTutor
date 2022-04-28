namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

public abstract class DomainEvent
{
    public DomainEvent() => OccurredOn = DateTime.UtcNow;

    public DateTime OccurredOn { get; }
}
