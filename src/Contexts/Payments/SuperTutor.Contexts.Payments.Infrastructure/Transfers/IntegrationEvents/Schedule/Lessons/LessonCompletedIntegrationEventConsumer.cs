using MassTransit;
using SuperTutor.Contexts.Payments.Application.Transfers.Commands.Create;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Shared.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Schedule.IntegrationEvents.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Infrastructure.Transfers.IntegrationEvents.Schedule.Lessons;

public class LessonCompletedIntegrationEventConsumer : IConsumer<LessonCompletedIntegrationEvent>
{
    private readonly ICommandHandler<CreateTransferCommand> createTransferCommandHandler;

    public LessonCompletedIntegrationEventConsumer(ICommandHandler<CreateTransferCommand> createTransferCommandHandler) => this.createTransferCommandHandler = createTransferCommandHandler;

    public async Task Consume(ConsumeContext<LessonCompletedIntegrationEvent> context)
        => await createTransferCommandHandler.Handle(new CreateTransferCommand(
                new ChargeId(context.Message.PaymentId),
                new LessonId(context.Message.LessonId),
                new StudentId(context.Message.StudentId),
                new TutorId(context.Message.TutorId)
            ), context.CancellationToken);
}
