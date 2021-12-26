using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.IncreaseRateForOneHour;

public class IncreaseProfileRateForOneHourCommand : Command
{
    public IncreaseProfileRateForOneHourCommand(Guid profileId, decimal increaseAmount)
    {
        ProfileId = profileId;
        IncreaseAmount = increaseAmount;
    }

    public Guid ProfileId { get; }

    public decimal IncreaseAmount { get; }
}
