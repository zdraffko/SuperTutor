using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RequestRedaction;

public class RequestTutorProfileRedactionCommand : Command
{
    public RequestTutorProfileRedactionCommand(Guid tutorProfileId, Guid adminId, string comment)
    {
        TutorProfileId = tutorProfileId;
        AdminId = adminId;
        Comment = comment;
    }

    public Guid TutorProfileId { get; }

    public Guid AdminId { get; }

    public string Comment { get; }
}
