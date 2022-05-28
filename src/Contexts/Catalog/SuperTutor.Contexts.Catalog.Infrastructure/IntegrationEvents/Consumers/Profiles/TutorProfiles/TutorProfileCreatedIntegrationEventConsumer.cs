using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.Create;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Models.ValueObjects;
using SuperTutor.Contexts.Catalog.Domain.Tutors;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles.TutorProfiles;

public class TutorProfileCreatedIntegrationEventConsumer : IConsumer<TutorProfileCreatedIntegrationEvent>
{
    private readonly ICommandHandler<CreateTutorProfileCommand> createTutorProfileCommandHandler;

    public TutorProfileCreatedIntegrationEventConsumer(ICommandHandler<CreateTutorProfileCommand> createTutorProfileCommandHandler) => this.createTutorProfileCommandHandler = createTutorProfileCommandHandler;

    public async Task Consume(ConsumeContext<TutorProfileCreatedIntegrationEvent> context)
        => await createTutorProfileCommandHandler.Handle(new CreateTutorProfileCommand(
                new TutorId(context.Message.TutorId),
                new TutorProfileId(context.Message.TutorProfileId),
                context.Message.About,
                new TutoringSubject(context.Message.TutoringSubject.Value, context.Message.TutoringSubject.Name),
                context.Message.TutoringGrades.Select(tutoringGrade => new TutoringGrade(tutoringGrade.Value, tutoringGrade.Name)),
                context.Message.RateForOneHour,
                context.Message.IsActive
            ), context.CancellationToken);
}
