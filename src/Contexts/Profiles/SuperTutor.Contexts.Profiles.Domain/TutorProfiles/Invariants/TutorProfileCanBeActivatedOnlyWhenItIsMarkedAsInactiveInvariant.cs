using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileCanBeActivatedOnlyWhenItIsMarkedAsInactiveInvariant : Invariant
{
    private readonly TutorProfileStatus status;

    public TutorProfileCanBeActivatedOnlyWhenItIsMarkedAsInactiveInvariant(TutorProfileStatus status)
        : base("The tutor profile can only be activated when it is marked as inactive")
        => this.status = status;

    public override bool IsValid() => status == TutorProfileStatus.Inactive;
}
