using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.UpdateAbout;

internal class UpdateProfileAboutCommandHandler : ICommandHandler<UpdateProfileAboutCommand>
{
    private readonly ITutorProfileRepository profileRepository;

    public UpdateProfileAboutCommandHandler(ITutorProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(UpdateProfileAboutCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(new TutorProfileId(command.ProfileId), cancellationToken);
        if (profile is null)
        {
            return Result.Fail("Profile not found.");
        }

        profile.UpdateAbout(command.NewAbout);

        return Result.Ok();
    }
}
