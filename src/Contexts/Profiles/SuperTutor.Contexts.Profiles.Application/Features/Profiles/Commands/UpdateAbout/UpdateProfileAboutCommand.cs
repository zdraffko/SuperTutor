using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.UpdateAbout;

public class UpdateProfileAboutCommand : Command
{
    public UpdateProfileAboutCommand(Guid profileId, string newAbout)
    {
        ProfileId = profileId;
        NewAbout = newAbout;
    }

    public Guid ProfileId { get; }

    public string NewAbout { get; }
}
