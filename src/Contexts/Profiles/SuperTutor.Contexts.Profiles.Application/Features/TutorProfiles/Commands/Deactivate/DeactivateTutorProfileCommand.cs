using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Deactivate;

public class DeactivateTutorProfileCommand : Command
{
    public DeactivateTutorProfileCommand(TutorProfileId tutorProfileId)
    {
        TutorProfileId = tutorProfileId;
    }

    public TutorProfileId TutorProfileId { get; }
}
