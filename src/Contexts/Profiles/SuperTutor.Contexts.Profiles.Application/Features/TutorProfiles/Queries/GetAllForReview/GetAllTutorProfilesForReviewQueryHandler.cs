using FluentResults;
using SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Queries.GetAllForReview;

internal class GetAllTutorProfilesForReviewQueryHandler : IQueryHandler<GetAllTutorProfilesForReviewQuery, GetAllTutorProfilesForReviewQueryPayload>
{
    private readonly ITutorProfilesQueryRepository tutorProfilesQueryRepository;

    public GetAllTutorProfilesForReviewQueryHandler(ITutorProfilesQueryRepository tutorProfilesQueryRepository) => this.tutorProfilesQueryRepository = tutorProfilesQueryRepository;

    public async Task<Result<GetAllTutorProfilesForReviewQueryPayload>> Handle(GetAllTutorProfilesForReviewQuery query, CancellationToken cancellationToken)
    {
        var tutorProfiles = await tutorProfilesQueryRepository.GetAllForReview(cancellationToken);
        var paylaod = new GetAllTutorProfilesForReviewQueryPayload(tutorProfiles);

        return Result.Ok(paylaod);
    }
}
