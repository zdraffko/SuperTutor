using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileStatusTransitionMustBeValid : DomainInvariant
{
    private readonly Status oldStatus;

    private readonly Status newStatus;

    public ProfileStatusTransitionMustBeValid(Status oldStatus, Status newStatus) 
        : base($"Profile status cannot transition from {oldStatus} to {newStatus}.")
    {
        this.oldStatus = oldStatus;
        this.newStatus = newStatus;
    }

    public override bool IsValid() => oldStatus.CanTransitionTo(newStatus);
}
