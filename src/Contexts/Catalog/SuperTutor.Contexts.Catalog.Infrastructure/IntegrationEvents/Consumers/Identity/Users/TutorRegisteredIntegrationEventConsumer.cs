using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Users.TutorRegistered;
using SuperTutor.Contexts.Catalog.Domain.Tutors;
using SuperTutor.Contexts.Identity.IntegrationEvents.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Identity.Users;

public class TutorRegisteredIntegrationEventConsumer : IConsumer<TutorRegisteredIntegrationEvent>
{
    private readonly ICommandHandler<CreateTutorCommand> createTutorCommandHandler;

    public TutorRegisteredIntegrationEventConsumer(ICommandHandler<CreateTutorCommand> createTutorCommandHandler) => this.createTutorCommandHandler = createTutorCommandHandler;

    public async Task Consume(ConsumeContext<TutorRegisteredIntegrationEvent> context)
        => await createTutorCommandHandler.Handle(
            new CreateTutorCommand(
                new TutorId(context.Message.UserId),
                context.Message.FirstName,
                context.Message.LastName),
            context.CancellationToken);
}
