using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.SubmitForReview;

public class SubmitTutorProfileForReviewCommand : Command
{
    public SubmitTutorProfileForReviewCommand(TutorProfileId tutorProfileId)
    {
        TutorProfileId = tutorProfileId;
    }

    public TutorProfileId TutorProfileId { get; }
}
