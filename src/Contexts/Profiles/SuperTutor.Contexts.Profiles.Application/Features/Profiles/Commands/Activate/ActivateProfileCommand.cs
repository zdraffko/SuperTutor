using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Activate;

public class ActivateProfileCommand : Command
{
    public ActivateProfileCommand(int profileId)
    {
        ProfileId = profileId;
    }

    public int ProfileId { get; }
}
