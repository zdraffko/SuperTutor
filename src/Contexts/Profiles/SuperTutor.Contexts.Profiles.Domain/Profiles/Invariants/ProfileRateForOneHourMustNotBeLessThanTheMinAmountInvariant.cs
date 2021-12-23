using SuperTutor.Contexts.Profiles.Domain.Profiles.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Invariants;

internal class ProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant : Invariant
{
    private readonly decimal newRateForOneHour;

    public ProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(decimal newRateForOneHour)
        : base($"The rate for one hour cannot be less than {ProfileConstants.RateForOneHourMinAmount}.")
    {
        this.newRateForOneHour = newRateForOneHour;
    }

    public override bool IsValid()
    {
        if (newRateForOneHour < ProfileConstants.RateForOneHourMinAmount)
        {
            return false;
        }

        return true;
    }
}
