using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Constants;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;

public class TutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant : Invariant
{
    private readonly decimal newRateForOneHour;

    public TutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant(decimal newRateForOneHour)
        : base($"The rate for one hour cannot be less than {TutorProfileConstants.RateForOneHourMinAmount}")
        => this.newRateForOneHour = newRateForOneHour;

    public override bool IsValid() => newRateForOneHour >= TutorProfileConstants.RateForOneHourMinAmount;
}
