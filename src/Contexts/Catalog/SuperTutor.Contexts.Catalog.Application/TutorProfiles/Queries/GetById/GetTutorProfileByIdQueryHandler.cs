using FluentResults;
using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetById;

internal class GetTutorProfileByIdQueryHandler : IQueryHandler<GetTutorProfileByIdQuery, GetTutorProfileByIdQueryPayload>
{
    private readonly ITutorProfilesQueryRepository tutorProfilesQueryRepository;

    public GetTutorProfileByIdQueryHandler(ITutorProfilesQueryRepository tutorProfilesQueryRepository) => this.tutorProfilesQueryRepository = tutorProfilesQueryRepository;

    public async Task<Result<GetTutorProfileByIdQueryPayload>> Handle(GetTutorProfileByIdQuery query, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfilesQueryRepository.GetById(query, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Профилът на учителят не бе намерен");
        }

        var payload = new GetTutorProfileByIdQueryPayload(tutorProfile);

        return Result.Ok(payload);
    }
}
