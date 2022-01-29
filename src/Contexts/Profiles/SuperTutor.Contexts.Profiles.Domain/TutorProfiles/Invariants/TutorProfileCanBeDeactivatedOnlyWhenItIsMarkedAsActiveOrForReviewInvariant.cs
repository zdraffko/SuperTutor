using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileCanBeDeactivatedOnlyWhenItIsMarkedAsActiveOrForReviewInvariant : Invariant
{
    private readonly TutorProfileStatus status;

    public TutorProfileCanBeDeactivatedOnlyWhenItIsMarkedAsActiveOrForReviewInvariant(TutorProfileStatus status)
        : base("The tutor profile can only be deactivated when it is marked as active or for review.")
    {
        this.status = status;
    }

    public override bool IsValid() => status == TutorProfileStatus.Active || status == TutorProfileStatus.ForReview;
}
