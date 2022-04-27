namespace SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

public abstract class DomainEvent
{
    public DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime occurredOn)
    {
        Id = id;
        OccurredOn = occurredOn;
    }

    public Guid Id { get; }

    public DateTime OccurredOn { get; }
}
