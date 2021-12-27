using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileCanBeRedactionRequestedOnlyWhenItIsMarkedAsForReviewInvariant : Invariant
{
    private readonly Status status;

    public ProfileCanBeRedactionRequestedOnlyWhenItIsMarkedAsForReviewInvariant(Status status)
        : base("Redaction can be requested for the profile only when it is marked as submitted for review.")
    {
        this.status = status;
    }

    public override bool IsValid() => status == Status.ForReview;
}
