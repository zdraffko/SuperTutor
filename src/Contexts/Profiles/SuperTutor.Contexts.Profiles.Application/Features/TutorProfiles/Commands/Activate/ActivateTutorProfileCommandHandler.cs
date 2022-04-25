using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Activate;

internal class ActivateTutorProfileCommandHandler : ICommandHandler<ActivateTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public ActivateTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository, IIntegrationEventsService integrationEventsService)
    {
        this.tutorProfileRepository = tutorProfileRepository;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(ActivateTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        tutorProfile.Activate();

        integrationEventsService.Raise(new TutorProfileActivatedIntegrationEvent(tutorProfile.Id.Value));

        return Result.Ok();
    }
}
