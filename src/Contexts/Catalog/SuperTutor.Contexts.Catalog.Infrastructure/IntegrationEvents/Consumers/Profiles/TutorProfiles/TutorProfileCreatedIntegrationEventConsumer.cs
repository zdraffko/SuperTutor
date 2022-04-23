using MassTransit;
using SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.CreateTutorProfile;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Infrastructure.IntegrationEvents.Consumers.Profiles.TutorProfiles;

public class TutorProfileCreatedIntegrationEventConsumer : IConsumer<TutorProfileCreatedIntegrationEvent>
{
    private readonly ICommandHandler<CreateTutorProfileCommand> createTutorProfileCommandHandler;

    public TutorProfileCreatedIntegrationEventConsumer(ICommandHandler<CreateTutorProfileCommand> createTutorProfileCommandHandler) => this.createTutorProfileCommandHandler = createTutorProfileCommandHandler;

    public async Task Consume(ConsumeContext<TutorProfileCreatedIntegrationEvent> context)
        => await createTutorProfileCommandHandler.Handle(new CreateTutorProfileCommand(
                new TutorProfileId(context.Message.TutorProfileId),
                context.Message.About,
                context.Message.TutoringSubject,
                context.Message.TutoringGrades,
                context.Message.RateForOneHour,
                context.Message.IsActive
            ), context.CancellationToken);
}
