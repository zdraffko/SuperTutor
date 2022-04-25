using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.Delete;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles.TutorProfiles;

public class TutorProfileDeletedIntegrationEventConsumer : IConsumer<TutorProfileDeletedIntegrationEvent>
{
    private readonly ICommandHandler<DeleteTutorProfileCommand> deleteTutorProfileCommandHandler;

    public TutorProfileDeletedIntegrationEventConsumer(ICommandHandler<DeleteTutorProfileCommand> deleteTutorProfileCommandHandler) => this.deleteTutorProfileCommandHandler = deleteTutorProfileCommandHandler;

    public async Task Consume(ConsumeContext<TutorProfileDeletedIntegrationEvent> context)
        => await deleteTutorProfileCommandHandler.Handle(new DeleteTutorProfileCommand(new TutorProfileId(context.Message.TutorProfileId)), context.CancellationToken);
}
