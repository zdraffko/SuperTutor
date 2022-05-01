using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Events;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Invariants;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Entities.Aggregates;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots;

public class TimeSlot : AggregateRoot<TimeSlotId, Guid>
{
    public static readonly TimeSpan Duration = TimeSpan.FromMinutes(30);

    // Required for loading the aggregate root from the event store 
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TimeSlot() : base(new TimeSlotId(Guid.Empty)) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private TimeSlot(TutorId tutorId, DateOnly date, TimeOnly startTime, TimeSlotType type) : base(new TimeSlotId(Guid.NewGuid()))
    {
        TutorId = tutorId;
        Date = date;
        StartTime = startTime;
        Type = type;
        Status = type == TimeSlotType.TimeOff ? TimeSlotStatus.Assigned : TimeSlotStatus.Unassigned;
    }

    public TutorId TutorId { get; private set; }

    public DateOnly Date { get; private set; }

    public TimeOnly StartTime { get; private set; }

    public TimeSlotType Type { get; private set; }

    public TimeSlotStatus Status { get; private set; }

    public static TimeSlot AddAvailability(TutorId tutorId, DateOnly date, TimeOnly startTime)
    {
        var addedTimeSlot = new TimeSlot(tutorId, date, startTime, TimeSlotType.Availability);

        addedTimeSlot.CheckInvariant(new TimeSlotDateAndTimeMustBeIntoTheFutureInvariant(date, startTime));

        addedTimeSlot.RaiseDomainEvent(new TimeSlotAvailabilityAddedDomainEvent(
            addedTimeSlot.Id,
            addedTimeSlot.TutorId,
            addedTimeSlot.Date,
            addedTimeSlot.StartTime,
            addedTimeSlot.Type.Value,
            addedTimeSlot.Status.Value
        ));

        return addedTimeSlot;
    }

    public static TimeSlot TakeTimeOff(TutorId tutorId, DateOnly date, TimeOnly startTime)
    {
        var addedTimeSlot = new TimeSlot(tutorId, date, startTime, TimeSlotType.TimeOff);

        addedTimeSlot.CheckInvariant(new TimeSlotDateAndTimeMustBeIntoTheFutureInvariant(date, startTime));

        addedTimeSlot.RaiseDomainEvent(new TimeSlotTimeOffTakenDomainEvent(
            addedTimeSlot.Id,
            addedTimeSlot.TutorId,
            addedTimeSlot.Date,
            addedTimeSlot.StartTime,
            addedTimeSlot.Type.Value,
            addedTimeSlot.Status.Value
        ));

        return addedTimeSlot;
    }

    public override void ApplyDomainEvent(DomainEvent domainEvent) => Apply((dynamic) domainEvent);

    private void Apply(TimeSlotAvailabilityAddedDomainEvent domainEvent)
    {
        Id = domainEvent.TimeSlotId;
        TutorId = domainEvent.TutorId;
        Date = domainEvent.Date;
        StartTime = domainEvent.StartTime;
        Type = Enumeration.FromValue<TimeSlotType>(domainEvent.Type)!;
        Status = Enumeration.FromValue<TimeSlotStatus>(domainEvent.Status)!;
    }

    private void Apply(TimeSlotTimeOffTakenDomainEvent domainEvent)
    {
        Id = domainEvent.TimeSlotId;
        TutorId = domainEvent.TutorId;
        Date = domainEvent.Date;
        StartTime = domainEvent.StartTime;
        Type = Enumeration.FromValue<TimeSlotType>(domainEvent.Type)!;
        Status = Enumeration.FromValue<TimeSlotStatus>(domainEvent.Status)!;
    }
}
