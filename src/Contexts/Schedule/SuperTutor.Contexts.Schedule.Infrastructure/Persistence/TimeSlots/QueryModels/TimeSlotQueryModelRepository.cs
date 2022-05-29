using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetForWeek;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;
using SuperTutor.Contexts.Schedule.Infrastructure.Persistence.Shared;

namespace SuperTutor.Contexts.Schedule.Infrastructure.Persistence.TimeSlots.QueryModels;

internal class TimeSlotQueryModelRepository : ITimeSlotQueryModelRepository
{
    private readonly ScheduleDbContext scheduleDbContext;

    public TimeSlotQueryModelRepository(ScheduleDbContext scheduleDbContext) => this.scheduleDbContext = scheduleDbContext;

    public async Task<IEnumerable<GetTimeSlotsForWeekQueryPayload.TimeSlot>> GetForWeek(GetTimeSlotsForWeekQuery query, CancellationToken cancellationToken)
        => await scheduleDbContext.TimeSlots
            .AsNoTracking()
            .Where(timeSlot
                => timeSlot.TutorId == query.TutorId
                && timeSlot.Date >= query.WeekStartDate
                && timeSlot.Date <= query.WeekStartDate.AddDays(7))
            .Select(timeSlot => new GetTimeSlotsForWeekQueryPayload.TimeSlot(
                    timeSlot.Id,
                    timeSlot.TutorId,
                    timeSlot.Date,
                    timeSlot.StartTime,
                    timeSlot.Type.Name,
                    timeSlot.Status.Name))
            .ToListAsync(cancellationToken);
}
