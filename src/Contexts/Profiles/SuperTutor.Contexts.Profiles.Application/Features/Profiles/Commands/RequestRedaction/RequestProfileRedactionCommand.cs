using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.RequestRedaction;

public class RequestProfileRedactionCommand : Command
{
    public RequestProfileRedactionCommand(int profileId)
    {
        ProfileId = profileId;
    }

    public int ProfileId { get; }
}
