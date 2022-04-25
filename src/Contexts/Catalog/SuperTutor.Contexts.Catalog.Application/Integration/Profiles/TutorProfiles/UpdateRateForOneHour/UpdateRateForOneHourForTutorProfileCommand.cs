using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.UpdateRateForOneHour;

public class UpdateRateForOneHourForTutorProfileCommand : Command
{
    public UpdateRateForOneHourForTutorProfileCommand(TutorProfileId tutorProfileId, decimal newRateForOneHour)
    {
        TutorProfileId = tutorProfileId;
        NewRateForOneHour = newRateForOneHour;
    }

    public TutorProfileId TutorProfileId { get; }

    public decimal NewRateForOneHour { get; }
}
