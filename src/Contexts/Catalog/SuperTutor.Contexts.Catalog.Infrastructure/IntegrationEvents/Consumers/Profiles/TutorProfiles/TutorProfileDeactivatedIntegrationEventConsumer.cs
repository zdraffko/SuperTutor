using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.DeactivateTutorProfile;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles.TutorProfiles;

public class TutorProfileDeactivatedIntegrationEventConsumer : IConsumer<TutorProfileDeactivatedIntegrationEvent>
{
    private readonly ICommandHandler<DeactivateTutorProfileCommand> deactivateTutorProfileCommandHandler;

    public TutorProfileDeactivatedIntegrationEventConsumer(ICommandHandler<DeactivateTutorProfileCommand> deactivateTutorProfileCommandHandler) => this.deactivateTutorProfileCommandHandler = deactivateTutorProfileCommandHandler;

    public async Task Consume(ConsumeContext<TutorProfileDeactivatedIntegrationEvent> context)
        => await deactivateTutorProfileCommandHandler.Handle(new DeactivateTutorProfileCommand(new TutorProfileId(context.Message.TutorProfileId)), context.CancellationToken);
}
