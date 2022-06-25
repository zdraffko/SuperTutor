using MassTransit;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Create;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.IntegrationEvents.Lessons;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Infrastructure.IntegrationEvents.Schedule.Lessons;

public class LessonStartedIntegrationEventConsumer : IConsumer<LessonStartedIntegrationEvent>
{
    private readonly ICommandHandler<CreateClassroomCommand> createClassroomCommandHandler;

    public LessonStartedIntegrationEventConsumer(ICommandHandler<CreateClassroomCommand> createClassroomCommandHandler) => this.createClassroomCommandHandler = createClassroomCommandHandler;

    public async Task Consume(ConsumeContext<LessonStartedIntegrationEvent> context)
        => await createClassroomCommandHandler.Handle(
            new CreateClassroomCommand(
                new LessonId(context.Message.LessonId),
                new TutorId(context.Message.TutorId),
                new StudentId(context.Message.StudentId)),
            context.CancellationToken);
}
