using FluentResults;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetForWeek;

internal class GetTimeSlotsForWeekQueryHandler : IQueryHandler<GetTimeSlotsForWeekQuery, GetTimeSlotsForWeekQueryPayload>
{
    private readonly ITimeSlotQueryModelRepository timeSlotQueryModelRepository;

    public GetTimeSlotsForWeekQueryHandler(ITimeSlotQueryModelRepository timeSlotQueryModelRepository) => this.timeSlotQueryModelRepository = timeSlotQueryModelRepository;

    public async Task<Result<GetTimeSlotsForWeekQueryPayload>> Handle(GetTimeSlotsForWeekQuery query, CancellationToken cancellationToken)
    {
        var timeSlots = await timeSlotQueryModelRepository.GetForWeek(query, cancellationToken);
        var payload = new GetTimeSlotsForWeekQueryPayload(timeSlots);

        return Result.Ok(payload);
    }
}
