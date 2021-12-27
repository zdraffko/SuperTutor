using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileCanBeSubmittedForReviewOnlyWhenItHasBeenModifiedSinceLastRedactionRequestInvariant : Invariant
{
    private readonly DateTime? lastModificationDate;
    private readonly DateTime? lastRedactionRequestDate;

    public ProfileCanBeSubmittedForReviewOnlyWhenItHasBeenModifiedSinceLastRedactionRequestInvariant(DateTime? lastModificationDate, DateTime? lastRedactionRequestDate)
        : base("The profile can only be submitted for review when it has been modified since the last redaction request.")
    {
        this.lastModificationDate = lastModificationDate;
        this.lastRedactionRequestDate = lastRedactionRequestDate;
    }

    public override bool IsValid()
    {
        if (!lastRedactionRequestDate.HasValue)
        {
            return true;
        }

        if (!lastModificationDate.HasValue)
        {
            return false;
        }

        return lastModificationDate.Value > lastRedactionRequestDate.Value;
    }
}
