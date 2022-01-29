using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Delete;

public class DeleteTutorProfileCommand : Command
{
    public DeleteTutorProfileCommand(TutorProfileId tutorProfileId)
    {
        TutorProfileId = tutorProfileId;
    }

    public TutorProfileId TutorProfileId { get; }
}
