using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;
using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetById;

namespace SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.Shared;

public interface ITutorProfilesQueryRepository
{
    Task<IEnumerable<GetTutorProfilesByFilterQueryPayload.TutorProfile>> GetByFilter(GetTutorProfilesByFilterQuery query, CancellationToken cancellationToken);

    Task<GetTutorProfileByIdQueryPayload.TutorProfile?> GetById(GetTutorProfileByIdQuery query, CancellationToken cancellationToken);
}
