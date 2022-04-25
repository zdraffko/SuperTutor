using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.Activate;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles.TutorProfiles;

public class TutorProfileApprovedIntegrationEventConsumer : IConsumer<TutorProfileApprovedIntegrationEvent>
{
    private readonly ICommandHandler<ActivateTutorProfileCommand> activateTutorProfileCommandHandler;

    public TutorProfileApprovedIntegrationEventConsumer(ICommandHandler<ActivateTutorProfileCommand> activateTutorProfileCommandHandler) => this.activateTutorProfileCommandHandler = activateTutorProfileCommandHandler;

    public async Task Consume(ConsumeContext<TutorProfileApprovedIntegrationEvent> context)
         => await activateTutorProfileCommandHandler.Handle(new ActivateTutorProfileCommand(new TutorProfileId(context.Message.TutorProfileId)), context.CancellationToken);
}
