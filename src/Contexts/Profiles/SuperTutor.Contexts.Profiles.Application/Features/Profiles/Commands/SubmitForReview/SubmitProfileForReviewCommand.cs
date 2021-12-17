using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.SubmitForReview;

public class SubmitProfileForReviewCommand : Command
{
    public SubmitProfileForReviewCommand(int profileId)
    {
        ProfileId = profileId;
    }

    public int ProfileId { get; }
}
