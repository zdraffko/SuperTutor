using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.UpdateAbout;

internal class UpdateTutorProfileAboutCommandHandler : ICommandHandler<UpdateTutorProfileAboutCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public UpdateTutorProfileAboutCommandHandler(ITutorProfileRepository tutorProfileRepository, IIntegrationEventsService integrationEventsService)
    {
        this.tutorProfileRepository = tutorProfileRepository;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(UpdateTutorProfileAboutCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        tutorProfile.UpdateAbout(command.NewAbout);

        integrationEventsService.Raise(new TutorProfileAboutUpdatedIntegrationEvent(tutorProfile.Id.Value, tutorProfile.About));

        return Result.Ok();
    }
}
