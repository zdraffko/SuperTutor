using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.UpdateAbout;

public class UpdateTutorProfileAboutCommand : Command
{
    public UpdateTutorProfileAboutCommand(Guid tutorProfileId, string newAbout)
    {
        TutorProfileId = tutorProfileId;
        NewAbout = newAbout;
    }

    public Guid TutorProfileId { get; }

    public string NewAbout { get; }
}
