using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.SubmitForReview;

public class SubmitProfileForReviewCommandHandler : ICommandHandler<SubmitProfileForReviewCommand>
{
    private readonly IProfileRepository profileRepository;

    public SubmitProfileForReviewCommandHandler(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(SubmitProfileForReviewCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new ProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        profile.SubmitForReview();

        return Result.Ok();
    }
}
