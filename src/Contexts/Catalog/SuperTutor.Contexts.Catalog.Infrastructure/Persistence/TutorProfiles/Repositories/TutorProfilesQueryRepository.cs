using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;
using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.Shared;
using SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Tutors;

namespace SuperTutor.Contexts.Catalog.Infrastructure.Persistence.TutorProfiles.Repositories;

internal class TutorProfilesQueryRepository : ITutorProfilesQueryRepository
{
    private readonly ITutorProfilesDbContext tutorProfilesDbContext;
    private readonly ITutorsDbContext tutorsDbContext;

    public TutorProfilesQueryRepository(ITutorProfilesDbContext tutorProfilesDbContext, ITutorsDbContext tutorsDbContext)
    {
        this.tutorProfilesDbContext = tutorProfilesDbContext;
        this.tutorsDbContext = tutorsDbContext;
    }

    public async Task<IEnumerable<GetTutorProfilesByFilterQueryPayload.TutorProfile>> GetByFilter(GetTutorProfilesByFilterQuery query, CancellationToken cancellationToken)
    {
        var queryable = tutorProfilesDbContext.TutorProfiles
            .AsNoTracking()
            .Where(tutorProfile => tutorProfile.IsActive);

        if (query.TutoringSubject.HasValue)
        {
            queryable = queryable.Where(tutorProfile => tutorProfile.TutoringSubject.Value == query.TutoringSubject.Value);
        }

        if (query.TutoringGrades is not null && query.TutoringGrades.Any())
        {
            queryable = queryable.Where(tutorProfile => tutorProfile.TutoringGrades.Any(tutoringGrade => query.TutoringGrades.Contains(tutoringGrade.Value)));
        }

        if (query.MinRateForOneHour.HasValue)
        {
            queryable = queryable.Where(tutorProfile => tutorProfile.RateForOneHour >= query.MinRateForOneHour);
        }

        if (query.MaxRateForOneHour.HasValue)
        {
            queryable = queryable.Where(tutorProfile => tutorProfile.RateForOneHour <= query.MaxRateForOneHour);
        }

        var tutorProfilesAndTutorsJoin = queryable.Join(
            tutorsDbContext.Tutors,
            tutorProfile => tutorProfile.TutorId,
            tutor => tutor.Id,
            (tutorProfile, tutor) => new
            {
                tutorProfile.Id,
                tutorProfile.TutorId,
                TutorFirstName = tutor.FirstName,
                TutorLastName = tutor.LastName,
                tutorProfile.About,
                tutorProfile.TutoringSubject,
                tutorProfile.TutoringGrades,
                tutorProfile.RateForOneHour
            });

        var queryResult = tutorProfilesAndTutorsJoin.Select(joinResult => new GetTutorProfilesByFilterQueryPayload.TutorProfile(
                joinResult.Id,
                joinResult.TutorId,
                joinResult.TutorFirstName,
                joinResult.TutorLastName,
                joinResult.About,
                joinResult.TutoringSubject.Name,
                joinResult.TutoringGrades.Select(tutoringGrade => tutoringGrade.Name),
                joinResult.RateForOneHour
            ));

        return await queryResult.ToListAsync(cancellationToken: cancellationToken);
    }
}
