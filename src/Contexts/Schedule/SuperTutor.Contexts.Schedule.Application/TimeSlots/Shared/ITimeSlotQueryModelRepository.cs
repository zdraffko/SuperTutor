using SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetAvailability;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetForWeek;
using SuperTutor.Contexts.Schedule.Domain.Common.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;

public interface ITimeSlotQueryModelRepository
{
    Task Create(TimeSlotQueryModel timeSlotQueryModel, CancellationToken cancellationToken);

    Task<IEnumerable<GetTimeSlotsForWeekQueryPayload.TimeSlot>> GetForWeek(GetTimeSlotsForWeekQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<GetTutorAvailabilityQueryPayload.Availability>> GetTutorAvailability(GetTutorAvailabilityQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<TimeSlotId>> GetIdsForLesson(TutorId tutorId, DateTime lessonStart, DateTime lessonEnd, CancellationToken cancellationToken);

    Task SetAvailabilityAsAssigned(TimeSlotId timeSlotId, CancellationToken cancellationToken);
}
