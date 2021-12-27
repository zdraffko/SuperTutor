using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileCanBeActivatedOnlyWhenItIsMarkedAsInactiveInvariant : Invariant
{
    private readonly Status status;

    public ProfileCanBeActivatedOnlyWhenItIsMarkedAsInactiveInvariant(Status status)
        : base("The profile can only be activated when it is marked as inactive.")
    {
        this.status = status;
    }

    public override bool IsValid() => status == Status.Inactive;
}
