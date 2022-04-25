using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.UpdateAbout;

public class UpdateAboutForTutorProfileCommand : Command
{
    public UpdateAboutForTutorProfileCommand(TutorProfileId tutorProfileId, string newAbout)
    {
        TutorProfileId = tutorProfileId;
        NewAbout = newAbout;
    }

    public TutorProfileId TutorProfileId { get; }

    public string NewAbout { get; }
}
