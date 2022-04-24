using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;
using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.Shared;

namespace SuperTutor.Contexts.Catalog.Persistence.TutorProfiles;

internal class TutorProfilesQueryRepository : ITutorProfilesQueryRepository
{
    private readonly ITutorProfilesDbContext tutorProfilesDbContext;

    public TutorProfilesQueryRepository(ITutorProfilesDbContext tutorProfilesDbContext) => this.tutorProfilesDbContext = tutorProfilesDbContext;

    public async Task<IEnumerable<GetTutorProfilesByFilterQueryPayload.TutorProfile>> GetByFilter(IEnumerable<int> tutoringGrades, IEnumerable<int> tutoringSubjects, decimal minRateForOneHour, decimal maxRateForOneHour, CancellationToken cancellationToken)
        => await tutorProfilesDbContext.TutorProfiles
            .AsNoTracking()
            .Where(tutorProfile =>
                tutorProfile.TutoringGrades.Any(tutoringGrade => tutoringGrades.Contains(tutoringGrade.Value)) &&
                tutoringSubjects.Contains(tutorProfile.TutoringSubject.Value) &&
                tutorProfile.RateForOneHour >= minRateForOneHour &&
                tutorProfile.RateForOneHour <= maxRateForOneHour &&
                tutorProfile.IsActive)
            .Select(tutorProfile => new GetTutorProfilesByFilterQueryPayload.TutorProfile(
                tutorProfile.About,
                tutorProfile.TutoringSubject.Name,
                tutorProfile.TutoringGrades.Select(tutoringGrade => tutoringGrade.Name),
                tutorProfile.RateForOneHour
                ))
            .ToListAsync(cancellationToken);
}
