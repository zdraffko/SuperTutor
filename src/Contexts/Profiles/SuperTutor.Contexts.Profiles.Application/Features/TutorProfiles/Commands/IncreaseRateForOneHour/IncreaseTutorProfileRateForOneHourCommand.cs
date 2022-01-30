using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.IncreaseRateForOneHour;

public class IncreaseTutorProfileRateForOneHourCommand : Command
{
    public IncreaseTutorProfileRateForOneHourCommand(TutorProfileId tutorProfileId, decimal increaseAmount)
    {
        TutorProfileId = tutorProfileId;
        IncreaseAmount = increaseAmount;
    }

    public TutorProfileId TutorProfileId { get; }

    public decimal IncreaseAmount { get; }
}
