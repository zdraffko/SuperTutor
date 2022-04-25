using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.UpdateRateForOneHour;

public class UpdateRateForOneHourCommand : Command
{
    public UpdateRateForOneHourCommand(TutorProfileId tutorProfileId, decimal newRateForOneHour)
    {
        TutorProfileId = tutorProfileId;
        NewRateForOneHour = newRateForOneHour;
    }

    public TutorProfileId TutorProfileId { get; }

    public decimal NewRateForOneHour { get; }
}
