using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Approve;

public class ApproveTutorProfileCommand : Command
{
    public ApproveTutorProfileCommand(Guid tutorProfileId, Guid adminId)
    {
        TutorProfileId = tutorProfileId;
        AdminId = adminId;
    }

    public Guid TutorProfileId { get; }

    public Guid AdminId { get; }
}
