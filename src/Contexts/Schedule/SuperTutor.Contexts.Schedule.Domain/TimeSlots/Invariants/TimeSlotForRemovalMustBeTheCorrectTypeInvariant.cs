using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Invariants;

public class TimeSlotForRemovalMustBeTheCorrectTypeInvariant : Invariant
{
    private readonly TimeSlot timeSlot;
    private readonly TimeSlotType expectedTimeSlotType;

    public TimeSlotForRemovalMustBeTheCorrectTypeInvariant(TimeSlot timeSlot, TimeSlotType expectedTimeSlotType)
        : base($"The time slot for removal had to be of type '{expectedTimeSlotType.Name}' but was {timeSlot.Type.Name}")
    {
        this.timeSlot = timeSlot;
        this.expectedTimeSlotType = expectedTimeSlotType;
    }

    public override bool IsValid() => timeSlot.Type == expectedTimeSlotType;
}
