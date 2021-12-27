using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileCanBeDeactivatedOnlyWhenItIsMarkedAsActiveOrForReviewInvariant : Invariant
{
    private readonly Status status;

    public ProfileCanBeDeactivatedOnlyWhenItIsMarkedAsActiveOrForReviewInvariant(Status status)
        : base("The profile can only be deactivated when it is marked as active or for review.")
    {
        this.status = status;
    }

    public override bool IsValid() => status == Status.Active || status == Status.ForReview;
}
