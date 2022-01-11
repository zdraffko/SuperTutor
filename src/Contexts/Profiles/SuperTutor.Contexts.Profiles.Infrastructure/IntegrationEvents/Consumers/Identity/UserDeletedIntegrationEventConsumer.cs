using MassTransit;
using SuperTutor.Contexts.Identity.IntegrationEvents.Users;

namespace SuperTutor.Contexts.Profiles.Application.IntegrationEventConsumers.Identity
{
    public class UserDeletedIntegrationEventConsumer : IConsumer<UserDeletedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<UserDeletedIntegrationEvent> context)
        {
            await Console.Out.WriteLineAsync($"---------------------- consumed {context.Message.UserId} {context.SentTime}");
        }
    }
}
