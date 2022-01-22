using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Integration.Identity.Commands.DeleteProfilesForUser;

public class DeleteProfilesForUserCommand : Command
{
    public DeleteProfilesForUserCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
