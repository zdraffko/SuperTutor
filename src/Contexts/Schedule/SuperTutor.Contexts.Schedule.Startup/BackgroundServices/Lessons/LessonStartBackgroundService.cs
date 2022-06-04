using SuperTutor.Contexts.Schedule.Application.Lessons.Commands.Start;
using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Schedule.Startup.BackgroundServices.Lessons;

public sealed class LessonStartBackgroundService : IHostedService, IDisposable
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<LessonStartBackgroundService> logger;
    private Timer timer = default!;
    private bool IsFirstRun = true;

    public LessonStartBackgroundService(IServiceProvider serviceProvider, ILogger<LessonStartBackgroundService> logger)
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
        logger.LogInformation("Starting Scheduled Lessons");

        // Aways check for lessons to be started on the first run when the app starts
        if (IsFirstRun)
        {
            logger.LogInformation("Starting Scheduled Lessons First Run");

            await StartScheduledLessons(cancellationToken);

            IsFirstRun = false;

            return;
        }

        // After the first run, only check for lessons to be started as certain times
        switch (DateTime.UtcNow.Minute)
        {
            // A lesson can be scheduled for xx:00, xx:15, xx:30 and xx:45. That is why we check for lessons to be started 5 minutes before those times (+1 minute error buffer)
            case 10:
            case 11:
            case 25:
            case 26:
            case 40:
            case 41:
            case 55:
            case 56:
                {
                    await StartScheduledLessons(cancellationToken);

                    return;
                }
            default:
                logger.LogInformation("Lessons start time not reached");

                return;
        }
    }

    private async Task StartScheduledLessons(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var lessonQueryModelRepository = scope.ServiceProvider.GetService<ILessonQueryModelRepository>();
        if (lessonQueryModelRepository is null)
        {
            throw new Exception($"{nameof(ILessonQueryModelRepository)} is not registered");
        }

        var startingLessonsIds = await lessonQueryModelRepository.GetStartingLessonsIds(cancellationToken);
        if (!startingLessonsIds.Any())
        {
            logger.LogInformation("No lessons for starting were found");

            return;
        }

        var startLessonCommandHandler = scope.ServiceProvider.GetService<ICommandHandler<StartLessonCommand>>();
        if (startLessonCommandHandler is null)
        {
            throw new Exception($"{nameof(ICommandHandler<StartLessonCommand>)} is not registered");
        }

        foreach (var startingLessonId in startingLessonsIds)
        {
            logger.LogInformation($"Starting lesson with Id {startingLessonId}");

            var startLessonCommand = new StartLessonCommand(startingLessonId);

            var startLessonCommandResult = await startLessonCommandHandler.Handle(startLessonCommand, cancellationToken);
            if (startLessonCommandResult.IsFailed)
            {
                logger.LogInformation($"Failed to start lesson with Id {startingLessonId}");

                return;
            }

            logger.LogInformation($"Started lesson with Id {startingLessonId}");
        }
    }
}
