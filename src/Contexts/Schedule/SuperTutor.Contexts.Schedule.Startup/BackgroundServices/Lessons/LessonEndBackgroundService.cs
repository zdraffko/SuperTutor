using SuperTutor.Contexts.Schedule.Application.Lessons.Commands.End;
using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Startup.BackgroundServices.Lessons;

public class LessonEndBackgroundService : IHostedService, IDisposable
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<LessonEndBackgroundService> logger;
    private Timer timer = default!;
    private bool IsFirstRun = true;

    public LessonEndBackgroundService(IServiceProvider serviceProvider, ILogger<LessonEndBackgroundService> logger)
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
        logger.LogInformation("Ending Lessons");

        // Aways check for lessons to be ended on the first run when the app starts
        if (true) // if (IsFirstRun) For demonstration purposes
        {
            logger.LogInformation("Ending Lessons Job First Run");

            await EndLessons(cancellationToken);

            IsFirstRun = false;

            return;
        }

        // After the first run, only check for lessons to be ended as certain times
        switch (DateTime.UtcNow.Minute)
        {
            // A lesson can end at xx:00, xx:15, xx:30 and xx:45. That is why we check for lessons to be ended 5 minutes after those times (+1 minute error buffer)
            case 5:
            case 6:
            case 20:
            case 21:
            case 35:
            case 36:
            case 50:
            case 51:
                {
                    await EndLessons(cancellationToken);

                    return;
                }
            default:
                logger.LogInformation("End lesson time not reached");

                return;
        }
    }

    private async Task EndLessons(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var lessonQueryModelRepository = scope.ServiceProvider.GetService<ILessonQueryModelRepository>();
        if (lessonQueryModelRepository is null)
        {
            throw new Exception($"{nameof(ILessonQueryModelRepository)} is not registered");
        }

        var endingLessonsIds = await lessonQueryModelRepository.GetEndingLessonsIds(cancellationToken);
        if (!endingLessonsIds.Any())
        {
            logger.LogInformation("No lessons for ending were found");

            return;
        }

        var endLessonCommandHandler = scope.ServiceProvider.GetService<ICommandHandler<EndLessonCommand>>();
        if (endLessonCommandHandler is null)
        {
            throw new Exception($"{nameof(ICommandHandler<EndLessonCommand>)} is not registered");
        }

        foreach (var endingLessonId in endingLessonsIds)
        {
            logger.LogInformation($"Ending lesson with Id {endingLessonId}");

            var endLessonCommand = new EndLessonCommand(endingLessonId);

            var endLessonCommandResult = await endLessonCommandHandler.Handle(endLessonCommand, cancellationToken);
            if (endLessonCommandResult.IsFailed)
            {
                logger.LogInformation($"Failed to end lesson with Id {endingLessonId}");

                return;
            }

            logger.LogInformation($"Ended lesson with Id {endingLessonId}");
        }
    }
}

