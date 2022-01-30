using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Activate;

public class ActivateTutorProfileCommand : Command
{
    public ActivateTutorProfileCommand(TutorProfileId tutorProfileId)
    {
        TutorProfileId = tutorProfileId;
    }

    public TutorProfileId TutorProfileId { get; }
}
