using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileCanBeActivatedOnlyWhenItHasNotBeenModifiedSinceLastApprovalInvariant : Invariant
{
    private readonly DateTime? lastModificationDate;
    private readonly DateTime lastApprovalDate;

    public TutorProfileCanBeActivatedOnlyWhenItHasNotBeenModifiedSinceLastApprovalInvariant(DateTime? lastModificationDate, DateTime lastApprovalDate)
        : base("The tutor profile can only be activated when there are no pending modifications for review")
    {
        this.lastModificationDate = lastModificationDate;
        this.lastApprovalDate = lastApprovalDate;
    }

    public override bool IsValid()
    {
        if (!lastModificationDate.HasValue)
        {
            return true;
        }

        return lastModificationDate.Value < lastApprovalDate;
    }
}
