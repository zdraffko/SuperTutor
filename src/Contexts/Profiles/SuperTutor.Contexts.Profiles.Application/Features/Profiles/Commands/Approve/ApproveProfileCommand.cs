using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Approve;

public class ApproveProfileCommand : Command
{
    public ApproveProfileCommand(int profileId)
    {
        ProfileId = profileId;
    }

    public int ProfileId { get; }
}
