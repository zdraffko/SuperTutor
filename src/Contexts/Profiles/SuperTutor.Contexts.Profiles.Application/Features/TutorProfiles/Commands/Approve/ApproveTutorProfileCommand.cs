using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Approve;

public class ApproveTutorProfileCommand : Command
{
    public ApproveTutorProfileCommand(TutorProfileId tutorProfileId, AdminId adminId)
    {
        TutorProfileId = tutorProfileId;
        AdminId = adminId;
    }

    public TutorProfileId TutorProfileId { get; }

    public AdminId AdminId { get; }
}
