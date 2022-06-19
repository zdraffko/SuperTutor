using SuperTutor.Contexts.Schedule.Domain.Common.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetForWeek;

public class GetTimeSlotsForWeekQuery : Query<GetTimeSlotsForWeekQueryPayload>
{
    public GetTimeSlotsForWeekQuery(TutorId tutorId, DateOnly weekStartDate)
    {
        TutorId = tutorId;
        WeekStartDate = weekStartDate;
    }

    public TutorId TutorId { get; }

    public DateOnly WeekStartDate { get; }
}
