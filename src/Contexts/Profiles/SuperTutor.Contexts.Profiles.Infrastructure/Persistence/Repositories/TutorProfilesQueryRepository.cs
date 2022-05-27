using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForTutor;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.Shared;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Profiles.Infrastructure.Persistence.Contexts.Contracts;

namespace SuperTutor.Contexts.Profiles.Infrastructure.Persistence.Repositories;

internal class TutorProfilesQueryRepository : ITutorProfilesQueryRepository
{
    private readonly ITutorProfilesDbContext tutorProfilesDbContext;

    public TutorProfilesQueryRepository(ITutorProfilesDbContext tutorProfilesDbContext) => this.tutorProfilesDbContext = tutorProfilesDbContext;

    public async Task<IEnumerable<GetAllTutorProfilesForTutorQueryPayload.TutorProfile>> GetAllForTutor(TutorId tutorId)
    {
        var queryResult = await tutorProfilesDbContext.TutorProfiles
            .AsNoTracking()
            .Where(tutorProfile => tutorProfile.TutorId == tutorId)
            .Select(tutorProfile => new
            {
                tutorProfile.Id,
                tutorProfile.About,
                tutorProfile.TutoringSubject,
                tutorProfile.TutoringGrades,
                tutorProfile.RateForOneHour
            })
            .ToListAsync();

        return queryResult.Select(tutorProfile => new GetAllTutorProfilesForTutorQueryPayload.TutorProfile(
                    tutorProfile.Id,
                    tutorProfile.About,
                    tutorProfile.TutoringSubject.Value,
                    tutorProfile.TutoringGrades.Select(tutoringGrade => tutoringGrade.Value),
                    tutorProfile.RateForOneHour));
    }
}
