using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant : Invariant
{
    private readonly Status status;

    public ProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant(Status status)
        : base("The profile can only be updated when it is marked as inactive or for redaction.")
    {
        this.status = status;
    }

    public override bool IsValid() => status == Status.Inactive || status == Status.ForRedaction;
}
