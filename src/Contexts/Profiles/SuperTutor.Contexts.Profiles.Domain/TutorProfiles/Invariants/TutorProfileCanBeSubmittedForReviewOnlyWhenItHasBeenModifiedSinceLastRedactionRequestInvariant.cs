using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileCanBeSubmittedForReviewOnlyWhenItHasBeenModifiedSinceLastRedactionRequestInvariant : Invariant
{
    private readonly DateTime? lastModificationDate;
    private readonly DateTime? lastRedactionRequestDate;

    public TutorProfileCanBeSubmittedForReviewOnlyWhenItHasBeenModifiedSinceLastRedactionRequestInvariant(DateTime? lastModificationDate, DateTime? lastRedactionRequestDate)
        : base("The tutor profile can only be submitted for review when it has been modified since the last redaction request.")
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
