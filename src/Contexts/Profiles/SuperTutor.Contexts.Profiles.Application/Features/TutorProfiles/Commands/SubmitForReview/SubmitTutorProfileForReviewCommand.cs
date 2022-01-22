using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.SubmitForReview;

public class SubmitTutorProfileForReviewCommand : Command
{
    public SubmitTutorProfileForReviewCommand(Guid tutorProfileId)
    {
        TutorProfileId = tutorProfileId;
    }

    public Guid TutorProfileId { get; }
}
