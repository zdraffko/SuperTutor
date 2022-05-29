using SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetForWeek;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;

public interface ITimeSlotQueryModelRepository
{
    Task Create(TimeSlotQueryModel timeSlotQueryModel, CancellationToken cancellationToken);

    Task<IEnumerable<GetTimeSlotsForWeekQueryPayload.TimeSlot>> GetForWeek(GetTimeSlotsForWeekQuery query, CancellationToken cancellationToken);
}
