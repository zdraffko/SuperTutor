using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Schedule.Application.TimeSlots.Queries.GetAvailability;

public class GetTutorAvailabilityQuery : Query<GetTutorAvailabilityQueryPayload>
{
    public GetTutorAvailabilityQuery(TutorId tutorId) => TutorId = tutorId;

    public TutorId TutorId { get; }
}
