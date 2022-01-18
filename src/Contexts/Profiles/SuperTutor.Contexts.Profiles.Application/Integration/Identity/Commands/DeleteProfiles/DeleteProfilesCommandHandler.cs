using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Integration.Identity.Commands.DeleteProfiles;

internal class DeleteProfilesCommandHandler : ICommandHandler<DeleteProfilesCommand>
{
    private readonly IProfileRepository profileRepository;

    public DeleteProfilesCommandHandler(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(DeleteProfilesCommand command, CancellationToken cancellationToken)
    {
        var allUserProfiles = await profileRepository.GetAllForUser(command.UserId, cancellationToken);

        foreach (var profile in allUserProfiles)
        {
            profileRepository.Remove(profile);
        }

        return Result.Ok();
    }
}
