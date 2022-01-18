using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Integration.Identity.Commands.DeleteProfiles;

public class DeleteProfilesCommand : Command
{
    public DeleteProfilesCommand(UserId userId)
    {
        UserId = userId;
    }

    public UserId UserId { get; }
}
