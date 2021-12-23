using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.UpdateAbout;

internal class UpdateProfileAboutCommandHandler : ICommandHandler<UpdateProfileAboutCommand>
{
    private readonly IProfileRepository profileRepository;

    public UpdateProfileAboutCommandHandler(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(UpdateProfileAboutCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new ProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        profile.UpdateAbout(command.NewAbout);

        return Result.Ok();
    }
}
