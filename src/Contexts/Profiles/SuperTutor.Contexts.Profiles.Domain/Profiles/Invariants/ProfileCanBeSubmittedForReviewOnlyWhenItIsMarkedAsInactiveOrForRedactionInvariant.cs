using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileCanBeSubmittedForReviewOnlyWhenItIsMarkedAsInactiveOrForRedactionInvariant : Invariant
{
    private readonly Status status;

    public ProfileCanBeSubmittedForReviewOnlyWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status status)
        : base("Profile can be submitted for review only when it is marked as inactive or for redaction.")
    {
        this.status = status;
    }

    public override bool IsValid() => status == Status.Inactive || status == Status.ForRedaction;
}
