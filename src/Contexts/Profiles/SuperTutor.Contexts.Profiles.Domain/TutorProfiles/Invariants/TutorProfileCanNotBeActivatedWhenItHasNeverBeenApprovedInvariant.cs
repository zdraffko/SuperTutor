using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileCanNotBeActivatedWhenItHasNeverBeenApprovedInvariant : Invariant
{
    private readonly DateTime? lastApprovalDate;

    public TutorProfileCanNotBeActivatedWhenItHasNeverBeenApprovedInvariant(DateTime? lastApprovalDate)
        : base("The tutor profile must be approved before it can be activated")
        => this.lastApprovalDate = lastApprovalDate;

    public override bool IsValid() => lastApprovalDate.HasValue;
}
