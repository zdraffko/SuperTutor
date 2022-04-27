using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots;

public class TimeSlotId : Identifier<Guid>
{
    public TimeSlotId(Guid value) : base(value)
    {
    }
}
