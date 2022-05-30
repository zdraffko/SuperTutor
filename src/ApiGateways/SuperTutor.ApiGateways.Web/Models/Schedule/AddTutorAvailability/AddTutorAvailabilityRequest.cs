namespace SuperTutor.ApiGateways.Web.Models.Schedule.AddTutorAvailability;

public class AddTutorAvailabilityRequest
{
    public AddTutorAvailabilityRequest(DateOnly date, TimeOnly startTime)
    {
        Date = date;
        StartTime = startTime;
    }

    public DateOnly Date { get; }

    public TimeOnly StartTime { get; }
}
