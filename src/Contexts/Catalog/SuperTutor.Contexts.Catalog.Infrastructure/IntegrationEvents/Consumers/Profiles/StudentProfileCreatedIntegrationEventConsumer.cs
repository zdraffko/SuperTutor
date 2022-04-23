using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.Commands.CreateStudent;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.Contexts.Profiles.IntegrationEvents.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles;

public class StudentProfileCreatedIntegrationEventConsumer : IConsumer<StudentProfileCreatedIntegrationEvent>
{
    private readonly ICommandHandler<CreateStudentCommand> createStudentCommandHandler;

    public StudentProfileCreatedIntegrationEventConsumer(ICommandHandler<CreateStudentCommand> createStudentCommandHandler) => this.createStudentCommandHandler = createStudentCommandHandler;

    public async Task Consume(ConsumeContext<StudentProfileCreatedIntegrationEvent> context)
        => await createStudentCommandHandler.Handle(new CreateStudentCommand(new StudentId(context.Message.StudentId)), context.CancellationToken);
}
