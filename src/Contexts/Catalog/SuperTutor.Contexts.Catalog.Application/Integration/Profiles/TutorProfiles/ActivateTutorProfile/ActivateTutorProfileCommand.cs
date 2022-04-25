using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.ActivateTutorProfile;

public class ActivateTutorProfileCommand : Command
{
    public ActivateTutorProfileCommand(TutorProfileId tutorProfileId) => TutorProfileId = tutorProfileId;

    public TutorProfileId TutorProfileId { get; }
}
