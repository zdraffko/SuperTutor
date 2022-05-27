using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForTutor;

public class GetAllTutorProfilesForTutorQuery : Query<GetAllTutorProfilesForTutorQueryPayload>
{
    public GetAllTutorProfilesForTutorQuery(TutorId tutorId) => TutorId = tutorId;

    public TutorId TutorId { get; }
}
