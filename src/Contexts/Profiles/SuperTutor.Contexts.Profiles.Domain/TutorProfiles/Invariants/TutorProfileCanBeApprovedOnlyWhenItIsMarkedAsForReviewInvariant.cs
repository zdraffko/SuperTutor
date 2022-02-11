using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileCanBeApprovedOnlyWhenItIsMarkedAsForReviewInvariant : Invariant
{
    private readonly TutorProfileStatus status;

    public TutorProfileCanBeApprovedOnlyWhenItIsMarkedAsForReviewInvariant(TutorProfileStatus status)
        : base("The tutor profile can only be approved when it is marked as submitted for review")
    {
        this.status = status;
    }

    public override bool IsValid() => status == TutorProfileStatus.ForReview;
}
