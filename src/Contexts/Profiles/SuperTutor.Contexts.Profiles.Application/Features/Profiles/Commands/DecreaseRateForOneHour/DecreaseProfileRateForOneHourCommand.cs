using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.DecreaseRateForOneHour;

public class DecreaseProfileRateForOneHourCommand : Command
{
    public DecreaseProfileRateForOneHourCommand(int profileId, decimal decreaseAmount)
    {
        ProfileId = profileId;
        DecreaseAmount = decreaseAmount;
    }

    public int ProfileId { get; }

    public decimal DecreaseAmount { get; }
}
