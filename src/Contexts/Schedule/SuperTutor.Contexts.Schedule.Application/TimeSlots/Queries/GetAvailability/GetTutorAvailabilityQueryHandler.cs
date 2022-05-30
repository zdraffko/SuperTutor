using FluentResults;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetAvailability;

internal class GetTutorAvailabilityQueryHandler : IQueryHandler<GetTutorAvailabilityQuery, GetTutorAvailabilityQueryPayload>
{
    private readonly ITimeSlotQueryModelRepository timeSlotQueryModelRepository;

    public GetTutorAvailabilityQueryHandler(ITimeSlotQueryModelRepository timeSlotQueryModelRepository) => this.timeSlotQueryModelRepository = timeSlotQueryModelRepository;

    public async Task<Result<GetTutorAvailabilityQueryPayload>> Handle(GetTutorAvailabilityQuery query, CancellationToken cancellationToken)
    {
        var availabilities = await timeSlotQueryModelRepository.GetTutorAvailability(query, cancellationToken);
        var payload = new GetTutorAvailabilityQueryPayload(availabilities);

        return Result.Ok(payload);
    }
}
