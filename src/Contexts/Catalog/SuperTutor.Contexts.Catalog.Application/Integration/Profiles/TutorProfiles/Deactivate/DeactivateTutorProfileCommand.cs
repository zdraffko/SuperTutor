using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.Deactivate;

public class DeactivateTutorProfileCommand : Command
{
    public DeactivateTutorProfileCommand(TutorProfileId tutorProfileId) => TutorProfileId = tutorProfileId;

    public TutorProfileId TutorProfileId { get; }
}
