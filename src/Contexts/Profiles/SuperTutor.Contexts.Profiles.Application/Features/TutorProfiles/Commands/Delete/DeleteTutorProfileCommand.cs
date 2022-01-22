using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Delete;

public class DeleteTutorProfileCommand : Command
{
    public DeleteTutorProfileCommand(Guid tutorProfileId)
    {
        TutorProfileId = tutorProfileId;
    }

    public Guid TutorProfileId { get; }
}
