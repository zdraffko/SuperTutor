using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;

public sealed class TimeSlotType : Enumeration
{
    public static readonly TimeSpan Duration = TimeSpan.FromMinutes(30);

    public TimeSlotType(int value, string name) : base(value, name) { }

    public static readonly TimeSlotType Availability = new(1, nameof(Availability));

    public static readonly TimeSlotType TimeOff = new(2, nameof(TimeOff));
}
