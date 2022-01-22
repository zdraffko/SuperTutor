using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.SubmitForReview;

internal class SubmitProfileForReviewCommandHandler : ICommandHandler<SubmitProfileForReviewCommand>
{
    private readonly ITutorProfileRepository profileRepository;

    public SubmitProfileForReviewCommandHandler(ITutorProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(SubmitProfileForReviewCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new TutorProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        profile.SubmitForReview();

        return Result.Ok();
    }
}
