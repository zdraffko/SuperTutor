using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.UpdateAbout;

public class UpdateTutorProfileAboutCommand : Command
{
    public UpdateTutorProfileAboutCommand(TutorProfileId tutorProfileId, string newAbout)
    {
        TutorProfileId = tutorProfileId;
        NewAbout = newAbout;
    }

    public TutorProfileId TutorProfileId { get; }

    public string NewAbout { get; }
}
