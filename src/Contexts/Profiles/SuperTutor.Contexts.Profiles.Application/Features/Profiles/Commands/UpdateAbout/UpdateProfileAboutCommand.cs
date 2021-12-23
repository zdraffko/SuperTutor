using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.UpdateAbout;

public class UpdateProfileAboutCommand : Command
{
    public UpdateProfileAboutCommand(int profileId, string newAbout)
    {
        ProfileId = profileId;
        NewAbout = newAbout;
    }

    public int ProfileId { get; }

    public string NewAbout { get; }
}
