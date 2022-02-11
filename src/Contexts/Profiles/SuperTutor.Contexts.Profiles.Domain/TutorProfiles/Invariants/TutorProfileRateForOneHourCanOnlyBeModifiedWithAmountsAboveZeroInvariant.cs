using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileRateForOneHourCanOnlyBeModifiedWithAmountsAboveZeroInvariant : Invariant
{
    private readonly decimal rateForOneHourModificationAmount;

    public TutorProfileRateForOneHourCanOnlyBeModifiedWithAmountsAboveZeroInvariant(decimal rateForOneHourModificationAmount)
        : base("The amount for modifying the rate for one hour must be above zero")
    {
        this.rateForOneHourModificationAmount = rateForOneHourModificationAmount;
    }

    public override bool IsValid() => rateForOneHourModificationAmount > 0;
}
