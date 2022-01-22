using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.DecreaseRateForOneHour;

public class DecreaseTutorProfileRateForOneHourCommand : Command
{
    public DecreaseTutorProfileRateForOneHourCommand(Guid tutorProfileId, decimal decreaseAmount)
    {
        TutorProfileId = tutorProfileId;
        DecreaseAmount = decreaseAmount;
    }

    public Guid TutorProfileId { get; }

    public decimal DecreaseAmount { get; }
}
