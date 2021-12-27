using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileCanBeActivatedOnlyWhenItHasNotBeenModifiedSinceLastApprovalInvariant : Invariant
{
    private readonly DateTime? lastModificationDate;
    private readonly DateTime? lastApprovalDate;

    public ProfileCanBeActivatedOnlyWhenItHasNotBeenModifiedSinceLastApprovalInvariant(DateTime? lastModificationDate, DateTime? lastApprovalDate)
        : base("The profile can only be activated when there are no pending modifications for review.")
    {
        this.lastModificationDate = lastModificationDate;
        this.lastApprovalDate = lastApprovalDate;
    }

    public override bool IsValid()
    {
        if (!lastApprovalDate.HasValue)
        {
            return false;
        }

        if (!lastModificationDate.HasValue)
        {
            return true;
        }

        return lastModificationDate.Value < lastApprovalDate.Value;
    }
}
