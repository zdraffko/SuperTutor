using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;

namespace SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.Shared;

public interface ITutorProfilesQueryRepository
{
    Task<IEnumerable<GetTutorProfilesByFilterQueryPayload.TutorProfile>> GetByFilter(IEnumerable<int> tutoringGrades, IEnumerable<int> tutoringSubjects, decimal minRateForOneHour, decimal maxRateForOneHour, CancellationToken cancellationToken);
}
