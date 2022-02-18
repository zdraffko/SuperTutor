using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileCanBeRedactionRequestedOnlyWhenItIsMarkedAsForReviewInvariant : Invariant
{
    private readonly TutorProfileStatus status;

    public TutorProfileCanBeRedactionRequestedOnlyWhenItIsMarkedAsForReviewInvariant(TutorProfileStatus status)
        : base("Redaction can be requested for the tutor profile only when it is marked as submitted for review")
        => this.status = status;

    public override bool IsValid() => status == TutorProfileStatus.ForReview;
}
