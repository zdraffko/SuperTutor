using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.IncreaseRateForOneHour;

internal class IncreaseTutorProfileRateForOneHourCommandHandler : ICommandHandler<IncreaseTutorProfileRateForOneHourCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public IncreaseTutorProfileRateForOneHourCommandHandler(ITutorProfileRepository tutorProfileRepository, IIntegrationEventsService integrationEventsService)
    {
        this.tutorProfileRepository = tutorProfileRepository;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(IncreaseTutorProfileRateForOneHourCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        tutorProfile.IncreaseRateForOneHour(command.IncreaseAmount);

        integrationEventsService.Raise(new TutorProfileRateForOneHourIncreasedIntegrationEvent(tutorProfile.Id.Value, tutorProfile.RateForOneHour));

        return Result.Ok();
    }
}
