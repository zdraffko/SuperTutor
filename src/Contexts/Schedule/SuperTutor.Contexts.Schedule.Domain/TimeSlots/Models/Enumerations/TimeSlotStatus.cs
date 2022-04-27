using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;

public sealed class TimeSlotStatus : Enumeration
{
    private TimeSlotStatus(int value, string name) : base(value, name) { }

    public static readonly TimeSlotStatus Assigned = new(1, nameof(Assigned));

    public static readonly TimeSlotStatus Unassigned = new(2, nameof(Unassigned));

    public static readonly TimeSlotStatus Removed = new(3, nameof(Removed));
}
