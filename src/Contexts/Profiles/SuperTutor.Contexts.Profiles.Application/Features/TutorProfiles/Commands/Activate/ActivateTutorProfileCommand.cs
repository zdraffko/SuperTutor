using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Activate;

public class ActivateTutorProfileCommand : Command
{
    public ActivateTutorProfileCommand(Guid tutorProfileId)
    {
        TutorProfileId = tutorProfileId;
    }

    public Guid TutorProfileId { get; }
}
