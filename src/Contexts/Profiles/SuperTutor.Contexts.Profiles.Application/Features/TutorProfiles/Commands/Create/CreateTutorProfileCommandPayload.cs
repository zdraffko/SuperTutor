using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Create;

public class CreateTutorProfileCommandPayload
{
    public CreateTutorProfileCommandPayload(TutorProfileId tutorProfileId) => TutorProfileId = tutorProfileId;

    public TutorProfileId TutorProfileId { get; }
}
