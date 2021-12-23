using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Deactivate;

public class DeactivateProfileCommand : Command
{
    public DeactivateProfileCommand(int profileId)
    {
        ProfileId = profileId;
    }

    public int ProfileId { get; }
}
