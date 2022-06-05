using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForReview;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForTutor;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.Shared;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Profiles.Infrastructure.Persistence.Contexts.Contracts;

namespace SuperTutor.Contexts.Profiles.Infrastructure.Persistence.Repositories;

internal class TutorProfilesQueryRepository : ITutorProfilesQueryRepository
{
    private readonly ITutorProfilesDbContext tutorProfilesDbContext;

    public TutorProfilesQueryRepository(ITutorProfilesDbContext tutorProfilesDbContext) => this.tutorProfilesDbContext = tutorProfilesDbContext;

    public async Task<IEnumerable<GetAllTutorProfilesForTutorQueryPayload.TutorProfile>> GetAllForTutor(TutorId tutorId, CancellationToken cancellationToken)
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
            .ToListAsync(cancellationToken);

        return queryResult.Select(tutorProfile => new GetAllTutorProfilesForTutorQueryPayload.TutorProfile(
                    tutorProfile.Id,
                    tutorProfile.About,
                    tutorProfile.TutoringSubject.Value,
                    tutorProfile.TutoringGrades.Select(tutoringGrade => tutoringGrade.Value),
                    tutorProfile.RateForOneHour));
    }

    public async Task<IEnumerable<GetAllTutorProfilesForReviewQueryPayload.TutorProfile>> GetAllForReview(CancellationToken cancellationToken)
    {
        // TODO - Fix the Where to not be on the client side. Currently there is a problem with comparing the statuses
        var queryResult = (await tutorProfilesDbContext.TutorProfiles
            .AsNoTracking()
            .ToListAsync(cancellationToken))
            .Where(tutorProfile => tutorProfile.Status == TutorProfileStatus.ForReview)
            .Select(tutorProfile => new
            {
                tutorProfile.Id,
                tutorProfile.About,
                tutorProfile.TutoringSubject,
                tutorProfile.TutoringGrades,
                tutorProfile.RateForOneHour,
                tutorProfile.Status
            });

        return queryResult.Select(tutorProfile => new GetAllTutorProfilesForReviewQueryPayload.TutorProfile(
                    tutorProfile.Id,
                    tutorProfile.About,
                    tutorProfile.TutoringSubject.Name,
                    tutorProfile.TutoringGrades.Select(tutoringGrade => tutoringGrade.Name),
                    tutorProfile.RateForOneHour,
                    tutorProfile.Status.Name));
    }
}
