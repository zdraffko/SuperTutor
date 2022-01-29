using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

internal class TutorProfileNewStatusMustHaveAValidTransitionFromTheOldStatusInvariant : Invariant
{
    private readonly TutorProfileStatus currentStatus;
    private readonly TutorProfileStatus newStatus;

    public TutorProfileNewStatusMustHaveAValidTransitionFromTheOldStatusInvariant(TutorProfileStatus currentStatus, TutorProfileStatus newStatus)
        : base($"Cannot changed the tutor profile status from '{currentStatus}' to '{newStatus}'")
    {
        this.currentStatus = currentStatus;
        this.newStatus = newStatus;
    }

    public override bool IsValid() => currentStatus.CanTransitionTo(newStatus);
}
