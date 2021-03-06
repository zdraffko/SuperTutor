using MassTransit;
using SuperTutor.Contexts.Identity.IntegrationEvents.Users;
using SuperTutor.Contexts.Payments.Application.Tutors.IntegrationEvents.Identity.Users.TutorRegistered;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Infrastructure.Tutors.IntegrationEvents.Identity.Users;

public class TutorRegisteredIntegrationEventConsumer : IConsumer<TutorRegisteredIntegrationEvent>
{
    private readonly ICommandHandler<CreateTutorCommand> createTutorCommandHandler;

    public TutorRegisteredIntegrationEventConsumer(ICommandHandler<CreateTutorCommand> createTutorCommandHandler) => this.createTutorCommandHandler = createTutorCommandHandler;

    public async Task Consume(ConsumeContext<TutorRegisteredIntegrationEvent> context)
        => await createTutorCommandHandler.Handle(
            new CreateTutorCommand(
                new TutorId(context.Message.UserId),
                context.Message.UserEmail,
                context.Message.FirstName,
                context.Message.LastName),
            context.CancellationToken);
}
