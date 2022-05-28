using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;

namespace SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.Shared;

public interface ITutorProfilesQueryRepository
{
    Task<IEnumerable<GetTutorProfilesByFilterQueryPayload.TutorProfile>> GetByFilter(GetTutorProfilesByFilterQuery query, CancellationToken cancellationToken);
}
