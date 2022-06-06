using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Queries.GetScheduledLessonsForTutor;

internal class GetScheduledLessonsForTutorQueryHandler : IQueryHandler<GetScheduledLessonsForTutorQuery, GetScheduledLessonsForTutorQueryPayload>
{
    private readonly ILessonQueryModelRepository lessonQueryModelRepository;

    public GetScheduledLessonsForTutorQueryHandler(ILessonQueryModelRepository lessonQueryModelRepository) => this.lessonQueryModelRepository = lessonQueryModelRepository;

    public async Task<Result<GetScheduledLessonsForTutorQueryPayload>> Handle(GetScheduledLessonsForTutorQuery query, CancellationToken cancellationToken)
    {
        var scheduledLesson = await lessonQueryModelRepository.GetScheduledLessonsForTutor(query, cancellationToken);
        var payload = new GetScheduledLessonsForTutorQueryPayload(scheduledLesson);

        return Result.Ok(payload);
    }
}
