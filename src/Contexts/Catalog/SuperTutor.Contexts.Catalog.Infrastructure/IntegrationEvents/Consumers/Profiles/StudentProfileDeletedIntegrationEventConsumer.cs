using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.Commands.DeleteStudent;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.Contexts.Profiles.IntegrationEvents.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles;

public class StudentProfileDeletedIntegrationEventConsumer : IConsumer<StudentProfileDeletedIntegrationEvent>
{
    private readonly ICommandHandler<DeleteStudentCommand> deleteStudentCommandHandler;

    public StudentProfileDeletedIntegrationEventConsumer(ICommandHandler<DeleteStudentCommand> deleteStudentCommandHandler) => this.deleteStudentCommandHandler = deleteStudentCommandHandler;

    public async Task Consume(ConsumeContext<StudentProfileDeletedIntegrationEvent> context)
        => await deleteStudentCommandHandler.Handle(new DeleteStudentCommand(new StudentId(context.Message.StudentId)), context.CancellationToken);
}
