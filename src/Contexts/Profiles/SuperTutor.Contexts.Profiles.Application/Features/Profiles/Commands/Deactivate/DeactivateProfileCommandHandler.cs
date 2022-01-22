using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Deactivate;

internal class DeactivateProfileCommandHandler : ICommandHandler<DeactivateProfileCommand>
{
    private readonly ITutorProfileRepository profileRepository;

    public DeactivateProfileCommandHandler(ITutorProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(DeactivateProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new TutorProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        profile.Deactivate();

        return Result.Ok();
    }
}
