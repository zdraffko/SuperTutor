using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.DecreaseRateForOneHour;

public class DecreaseTutorProfileRateForOneHourCommand : Command
{
    public DecreaseTutorProfileRateForOneHourCommand(TutorProfileId tutorProfileId, decimal decreaseAmount)
    {
        TutorProfileId = tutorProfileId;
        DecreaseAmount = decreaseAmount;
    }

    public TutorProfileId TutorProfileId { get; }

    public decimal DecreaseAmount { get; }
}
