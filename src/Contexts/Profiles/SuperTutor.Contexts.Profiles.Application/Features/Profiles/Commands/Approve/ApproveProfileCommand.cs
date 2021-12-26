using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Approve;

public class ApproveProfileCommand : Command
{
    public ApproveProfileCommand(Guid profileId, Guid adminId)
    {
        ProfileId = profileId;
        AdminId = adminId;
    }

    public Guid ProfileId { get; }

    public Guid AdminId { get; }
}
