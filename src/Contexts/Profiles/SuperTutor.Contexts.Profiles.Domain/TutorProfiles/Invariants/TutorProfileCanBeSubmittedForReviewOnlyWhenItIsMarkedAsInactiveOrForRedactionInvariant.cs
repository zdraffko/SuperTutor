using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileCanBeSubmittedForReviewOnlyWhenItIsMarkedAsInactiveOrForRedactionInvariant : Invariant
{
    private readonly TutorProfileStatus status;

    public TutorProfileCanBeSubmittedForReviewOnlyWhenItIsMarkedAsInactiveOrForRedactionInvariant(TutorProfileStatus status)
        : base("The tutor profile can be submitted for review only when it is marked as inactive or for redaction")
    {
        this.status = status;
    }

    public override bool IsValid() => status == TutorProfileStatus.Inactive || status == TutorProfileStatus.ForRedaction;
}
