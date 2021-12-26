using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RequestRedaction;

public class RequestProfileRedactionCommand : Command
{
    public RequestProfileRedactionCommand(Guid profileId, Guid adminId, string comment)
    {
        ProfileId = profileId;
        AdminId = adminId;
        Comment = comment;
    }

    public Guid ProfileId { get; }

    public Guid AdminId { get; }

    public string Comment { get; }
}
