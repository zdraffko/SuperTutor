using MassTransit;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Close;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.IntegrationEvents.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Infrastructure.IntegrationEvents.Schedule.Lessons;

public class LessonEndedIntegrationEventConsumer : IConsumer<LessonEndedIntegrationEvent>
{
    private readonly ICommandHandler<CloseClassroomCommand> automaticallyCloseClassroomCommandHandler;

    public LessonEndedIntegrationEventConsumer(ICommandHandler<CloseClassroomCommand> automaticallyCloseClassroomCommandHandler) => this.automaticallyCloseClassroomCommandHandler = automaticallyCloseClassroomCommandHandler;

    public async Task Consume(ConsumeContext<LessonEndedIntegrationEvent> context)
        => await automaticallyCloseClassroomCommandHandler.Handle(new CloseClassroomCommand(new LessonId(context.Message.LessonId)), context.CancellationToken);
}
