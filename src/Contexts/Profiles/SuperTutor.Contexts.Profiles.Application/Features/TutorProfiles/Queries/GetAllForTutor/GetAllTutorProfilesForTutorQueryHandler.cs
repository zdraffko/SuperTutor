using FluentResults;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForTutor;

internal class GetAllTutorProfilesForTutorQueryHandler : IQueryHandler<GetAllTutorProfilesForTutorQuery, GetAllTutorProfilesForTutorQueryPayload>
{
    private readonly ITutorProfilesQueryRepository tutorProfilesQueryRepository;

    public GetAllTutorProfilesForTutorQueryHandler(ITutorProfilesQueryRepository tutorProfilesQueryRepository) => this.tutorProfilesQueryRepository = tutorProfilesQueryRepository;

    public async Task<Result<GetAllTutorProfilesForTutorQueryPayload>> Handle(GetAllTutorProfilesForTutorQuery query, CancellationToken cancellationToken)
    {
        var tutorProfiles = await tutorProfilesQueryRepository.GetAllForTutor(query.TutorId);
        var paylaod = new GetAllTutorProfilesForTutorQueryPayload(tutorProfiles);

        return Result.Ok(paylaod);
    }
}
