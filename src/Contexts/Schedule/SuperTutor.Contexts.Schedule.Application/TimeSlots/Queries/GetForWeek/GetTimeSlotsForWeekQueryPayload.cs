using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetForWeek;

public class GetTimeSlotsForWeekQueryPayload
{
    public GetTimeSlotsForWeekQueryPayload(IEnumerable<TimeSlot> timeSlotsForWeek) => TimeSlotsForWeek = timeSlotsForWeek;

    public IEnumerable<TimeSlot> TimeSlotsForWeek { get; }

    public class TimeSlot
    {
        public TimeSlot(TimeSlotId id, TutorId tutorId, DateOnly date, TimeOnly startTime, string type, string status)
        {
            Id = id;
            TutorId = tutorId;
            Date = date;
            StartTime = startTime;
            Type = type;
            Status = status;
        }

        public TimeSlotId Id { get; }

        public TutorId TutorId { get; }

        public DateOnly Date { get; }

        public TimeOnly StartTime { get; }

        public string Type { get; }

        public string Status { get; }
    }
}
