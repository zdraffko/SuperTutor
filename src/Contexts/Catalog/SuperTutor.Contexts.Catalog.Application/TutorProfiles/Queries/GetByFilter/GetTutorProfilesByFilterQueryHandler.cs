using FluentResults;
using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;

internal class GetTutorProfilesByFilterQueryHandler : IQueryHandler<GetTutorProfilesByFilterQuery, GetTutorProfilesByFilterQueryPayload>
{
    private readonly ITutorProfilesQueryRepository tutorProfilesQueryRepository;

    public GetTutorProfilesByFilterQueryHandler(ITutorProfilesQueryRepository tutorProfilesQueryRepository) => this.tutorProfilesQueryRepository = tutorProfilesQueryRepository;

    public async Task<Result<GetTutorProfilesByFilterQueryPayload>> Handle(GetTutorProfilesByFilterQuery query, CancellationToken cancellationToken)
    {
        var tutorProfiles = await tutorProfilesQueryRepository.GetByFilter(query.TutoringGrades, query.TutoringSubjects, query.MinRateForOneHour, query.MaxRateForOneHour, cancellationToken);
        var payload = new GetTutorProfilesByFilterQueryPayload(tutorProfiles);

        return Result.Ok(payload);
    }
}
