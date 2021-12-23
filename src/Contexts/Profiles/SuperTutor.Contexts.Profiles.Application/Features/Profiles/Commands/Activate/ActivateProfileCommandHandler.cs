using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Activate;

internal class ActivateProfileCommandHandler : ICommandHandler<ActivateProfileCommand>
{
    private readonly IProfileRepository profileRepository;

    public ActivateProfileCommandHandler(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(ActivateProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new ProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        profile.Activate();

        return Result.Ok();
    }
}
