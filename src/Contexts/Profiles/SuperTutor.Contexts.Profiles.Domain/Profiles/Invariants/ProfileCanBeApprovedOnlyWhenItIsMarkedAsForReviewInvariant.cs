using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileCanBeApprovedOnlyWhenItIsMarkedAsForReviewInvariant : Invariant
{
    private readonly Status status;

    public ProfileCanBeApprovedOnlyWhenItIsMarkedAsForReviewInvariant(Status status)
        : base("The profile can only be approved when it is marked as submitted for review.")
    {
        this.status = status;
    }

    public override bool IsValid() => status == Status.ForReview;
}
