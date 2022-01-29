using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant : Invariant
{
    private readonly TutorProfileStatus status;

    public TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(TutorProfileStatus status)
        : base("The tutor profile can only be updated when it is marked as inactive or for redaction.")
    {
        this.status = status;
    }

    public override bool IsValid() => status == TutorProfileStatus.Inactive || status == TutorProfileStatus.ForRedaction;
}
