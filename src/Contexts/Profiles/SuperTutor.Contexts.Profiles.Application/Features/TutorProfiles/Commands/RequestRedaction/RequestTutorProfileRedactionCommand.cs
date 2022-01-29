using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RequestRedaction;

public class RequestTutorProfileRedactionCommand : Command
{
    public RequestTutorProfileRedactionCommand(TutorProfileId tutorProfileId, AdminId adminId, string comment)
    {
        TutorProfileId = tutorProfileId;
        AdminId = adminId;
        Comment = comment;
    }

    public TutorProfileId TutorProfileId { get; }

    public AdminId AdminId { get; }

    public string Comment { get; }
}
