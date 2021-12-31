using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Delete;

public class DeleteProfileCommand : Command
{
    public DeleteProfileCommand(Guid profileId)
    {
        ProfileId = profileId;
    }

    public Guid ProfileId { get; }
}
