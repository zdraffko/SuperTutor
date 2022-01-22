using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Deactivate;

public class DeactivateTutorProfileCommand : Command
{
    public DeactivateTutorProfileCommand(Guid tutorProfileId)
    {
        TutorProfileId = tutorProfileId;
    }

    public Guid TutorProfileId { get; }
}
