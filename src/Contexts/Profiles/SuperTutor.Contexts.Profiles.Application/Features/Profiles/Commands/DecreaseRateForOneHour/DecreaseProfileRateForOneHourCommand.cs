using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.DecreaseRateForOneHour;

public class DecreaseProfileRateForOneHourCommand : Command
{
    public DecreaseProfileRateForOneHourCommand(Guid profileId, decimal decreaseAmount)
    {
        ProfileId = profileId;
        DecreaseAmount = decreaseAmount;
    }

    public Guid ProfileId { get; }

    public decimal DecreaseAmount { get; }
}
