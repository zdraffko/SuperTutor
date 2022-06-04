using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetAvailability;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetForWeek;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;
using SuperTutor.Contexts.Schedule.Infrastructure.Shared.Persistence;

namespace SuperTutor.Contexts.Schedule.Infrastructure.TimeSlots.Persistence.QueryModels;

internal class TimeSlotQueryModelRepository : ITimeSlotQueryModelRepository
{
    private readonly ScheduleDbContext scheduleDbContext;

    public TimeSlotQueryModelRepository(ScheduleDbContext scheduleDbContext) => this.scheduleDbContext = scheduleDbContext;

    public async Task Create(TimeSlotQueryModel timeSlotQueryModel, CancellationToken cancellationToken)
    {
        scheduleDbContext.TimeSlots.Add(timeSlotQueryModel);

        await scheduleDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<GetTimeSlotsForWeekQueryPayload.TimeSlot>> GetForWeek(GetTimeSlotsForWeekQuery query, CancellationToken cancellationToken)
    {
        var res = await scheduleDbContext.TimeSlots
            .AsNoTracking()
            .Where(timeSlot
                => timeSlot.TutorId == query.TutorId
                && timeSlot.Date >= query.WeekStartDate.ToDateTime(TimeOnly.MinValue)
                && timeSlot.Date <= query.WeekStartDate.AddDays(7).ToDateTime(TimeOnly.MinValue))
            .ToListAsync(cancellationToken);

        return res.Select(timeSlot => new GetTimeSlotsForWeekQueryPayload.TimeSlot(
                    timeSlot.Id,
                    timeSlot.TutorId,
                    DateOnly.FromDateTime(timeSlot.Date),
                    TimeOnly.FromTimeSpan(timeSlot.StartTime),
                    timeSlot.Type,
                    timeSlot.Status))
                .OrderBy(timeSlot => timeSlot.Date)
                .ThenBy(timeSlot => timeSlot.StartTime);
    }

    public async Task<IEnumerable<GetTutorAvailabilityQueryPayload.Availability>> GetTutorAvailability(GetTutorAvailabilityQuery query, CancellationToken cancellationToken)
    {
        var queryResult = await scheduleDbContext.TimeSlots
            .AsNoTracking()
            .Where(timeSlot
                => timeSlot.TutorId == query.TutorId
                && timeSlot.Date >= DateTime.UtcNow
                && timeSlot.Type == "Availability"
                && timeSlot.Status == "Unassigned")
            .ToListAsync(cancellationToken);

        return queryResult.GroupBy(timeSlot => timeSlot.Date.Date)
                .Select(group => new GetTutorAvailabilityQueryPayload.Availability(DateOnly.FromDateTime(group.Key), group.Select(timeSlot => TimeOnly.FromTimeSpan(timeSlot.StartTime)).OrderBy(startTime => startTime)))
                .OrderBy(availability => availability.Date)
                .ToList();
    }
}
