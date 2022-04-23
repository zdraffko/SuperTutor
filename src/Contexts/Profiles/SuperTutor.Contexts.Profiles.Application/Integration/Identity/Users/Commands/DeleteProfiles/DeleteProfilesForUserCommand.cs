using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Integration.Identity.Users.Commands.DeleteProfiles;

public class DeleteProfilesForUserCommand : Command
{
    public DeleteProfilesForUserCommand(Guid userId) => UserId = userId;

    public Guid UserId { get; }
}
