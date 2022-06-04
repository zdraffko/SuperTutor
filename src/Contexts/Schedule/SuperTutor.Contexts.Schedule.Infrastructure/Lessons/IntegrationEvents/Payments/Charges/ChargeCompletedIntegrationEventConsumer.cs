using MassTransit;
using SuperTutor.Contexts.Payments.IntegrationEvents.Charges;
using SuperTutor.Contexts.Schedule.Application.Lessons.IntegrationEvents.Payments.Charges.ChargeCompleted;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Infrastructure.Lessons.IntegrationEvents.Payments.Charges;

public class ChargeCompletedIntegrationEventConsumer : IConsumer<ChargeCompletedIntegrationEvent>
{
    private readonly ICommandHandler<ScheduleLessonCommand> scheduleLessonCommandHandler;

    public ChargeCompletedIntegrationEventConsumer(ICommandHandler<ScheduleLessonCommand> scheduleLessonCommandHandler) => this.scheduleLessonCommandHandler = scheduleLessonCommandHandler;

    public async Task Consume(ConsumeContext<ChargeCompletedIntegrationEvent> context)
        => await scheduleLessonCommandHandler.Handle(new ScheduleLessonCommand(new LessonId(context.Message.LessonId), new PaymentId(context.Message.ChargeId)), context.CancellationToken);
}
