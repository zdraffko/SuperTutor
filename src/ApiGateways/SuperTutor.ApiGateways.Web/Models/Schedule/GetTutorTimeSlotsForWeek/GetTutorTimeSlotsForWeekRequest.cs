namespace SuperTutor.ApiGateways.Web.Models.Schedule.GetTutorTimeSlotsForWeek;

public class GetTutorTimeSlotsForWeekRequest
{
    public GetTutorTimeSlotsForWeekRequest(DateOnly weekStartDate) => WeekStartDate = weekStartDate;

    public DateOnly WeekStartDate { get; }
}
