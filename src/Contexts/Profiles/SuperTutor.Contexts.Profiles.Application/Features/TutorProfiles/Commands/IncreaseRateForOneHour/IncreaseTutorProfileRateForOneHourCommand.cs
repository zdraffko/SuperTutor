using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.IncreaseRateForOneHour;

public class IncreaseTutorProfileRateForOneHourCommand : Command
{
    public IncreaseTutorProfileRateForOneHourCommand(Guid tutorProfileId, decimal increaseAmount)
    {
        TutorProfileId = tutorProfileId;
        IncreaseAmount = increaseAmount;
    }

    public Guid TutorProfileId { get; }

    public decimal IncreaseAmount { get; }
}
