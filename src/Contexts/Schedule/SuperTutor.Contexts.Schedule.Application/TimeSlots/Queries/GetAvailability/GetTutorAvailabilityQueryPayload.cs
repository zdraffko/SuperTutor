namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetAvailability;

public class GetTutorAvailabilityQueryPayload
{
    public GetTutorAvailabilityQueryPayload(IEnumerable<Availability> availabilities) => Availabilities = availabilities;

    public IEnumerable<Availability> Availabilities { get; }

    public class Availability
    {
        public Availability(DateOnly date, IEnumerable<TimeOnly> hours)
        {
            Date = date;
            Hours = hours;
        }

        public DateOnly Date { get; }

        public IEnumerable<TimeOnly> Hours { get; }
    }
}
