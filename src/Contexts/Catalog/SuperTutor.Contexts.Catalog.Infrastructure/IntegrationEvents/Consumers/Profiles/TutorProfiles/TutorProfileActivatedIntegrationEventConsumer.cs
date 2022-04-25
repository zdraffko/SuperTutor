using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.ActivateTutorProfile;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles.TutorProfiles;

public class TutorProfileActivatedIntegrationEventConsumer : IConsumer<TutorProfileActivatedIntegrationEvent>
{
    private readonly ICommandHandler<ActivateTutorProfileCommand> activateTutorProfileCommandHandler;

    public TutorProfileActivatedIntegrationEventConsumer(ICommandHandler<ActivateTutorProfileCommand> activateTutorProfileCommandHandler) => this.activateTutorProfileCommandHandler = activateTutorProfileCommandHandler;

    public async Task Consume(ConsumeContext<TutorProfileActivatedIntegrationEvent> context)
        => await activateTutorProfileCommandHandler.Handle(new ActivateTutorProfileCommand(new TutorProfileId(context.Message.TutorProfileId)), context.CancellationToken);
}
