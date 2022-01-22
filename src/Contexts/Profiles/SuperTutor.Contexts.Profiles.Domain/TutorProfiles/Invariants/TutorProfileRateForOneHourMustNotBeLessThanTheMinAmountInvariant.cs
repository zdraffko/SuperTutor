using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

internal class TutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant : Invariant
{
    private readonly decimal newRateForOneHour;

    public TutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(decimal newRateForOneHour)
        : base($"The rate for one hour cannot be less than {TutorProfileConstants.RateForOneHourMinAmount}.")
    {
        this.newRateForOneHour = newRateForOneHour;
    }

    public override bool IsValid()
    {
        if (newRateForOneHour < TutorProfileConstants.RateForOneHourMinAmount)
        {
            return false;
        }

        return true;
    }
}
