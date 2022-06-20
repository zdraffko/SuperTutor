using SuperTutor.Contexts.Schedule.Application.Lessons.Commands.Abandon;
using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Startup.BackgroundServices.Lessons;

public class LessonAbandonBackgroundService : IHostedService, IDisposable
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<LessonAbandonBackgroundService> logger;
    private Timer timer = default!;

    public LessonAbandonBackgroundService(IServiceProvider serviceProvider, ILogger<LessonAbandonBackgroundService> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        timer = new Timer(async (object? timerState) => await DoTimedWork(cancellationToken), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        timer.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose() => timer.Dispose();

    private async Task DoTimedWork(CancellationToken cancellationToken)
    {
        logger.LogInformation("Abandoning Lessons");

        await AbandonLessons(cancellationToken);
    }

    private async Task AbandonLessons(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var lessonQueryModelRepository = scope.ServiceProvider.GetService<ILessonQueryModelRepository>();
        if (lessonQueryModelRepository is null)
        {
            throw new Exception($"{nameof(ILessonQueryModelRepository)} is not registered");
        }

        var abandonedLessonsIds = await lessonQueryModelRepository.GetAbandonedLessonsIds(cancellationToken);
        if (!abandonedLessonsIds.Any())
        {
            logger.LogInformation("No abandoned lessons were found");

            return;
        }

        var abandonLessonCommandHandler = scope.ServiceProvider.GetService<ICommandHandler<AbandonLessonCommand>>();
        if (abandonLessonCommandHandler is null)
        {
            throw new Exception($"{nameof(ICommandHandler<AbandonLessonCommand>)} is not registered");
        }

        foreach (var abandonedLessonId in abandonedLessonsIds)
        {
            logger.LogInformation($"Abandoning lesson with Id {abandonedLessonId}");

            var abandonLessonCommand = new AbandonLessonCommand(abandonedLessonId);

            var abandonLessonCommandResult = await abandonLessonCommandHandler.Handle(abandonLessonCommand, cancellationToken);
            if (abandonLessonCommandResult.IsFailed)
            {
                logger.LogInformation($"Failed to abandon lesson with Id {abandonedLessonId}");

                return;
            }

            logger.LogInformation($"Abandoned lesson with Id {abandonedLessonId}");
        }
    }
}
