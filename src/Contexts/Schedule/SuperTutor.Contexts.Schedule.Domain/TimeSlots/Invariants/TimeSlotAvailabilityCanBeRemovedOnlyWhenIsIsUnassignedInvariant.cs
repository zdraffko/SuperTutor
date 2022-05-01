using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Invariants;

public class TimeSlotAvailabilityCanBeRemovedOnlyWhenIsIsUnassignedInvariant : Invariant
{
    private readonly TimeSlot timeSlot;

    public TimeSlotAvailabilityCanBeRemovedOnlyWhenIsIsUnassignedInvariant(TimeSlot timeSlot)
        : base("Availability time slot must be unassigned before it can be removed") => this.timeSlot = timeSlot;

    public override bool IsValid() => timeSlot.Status == TimeSlotStatus.Unassigned;
}
