using FluentResults;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Schedule.Application.Lessons.Queries.GetScheduledLessonsForStudent;

internal class GetScheduledLessonsForStudentQueryHandler : IQueryHandler<GetScheduledLessonsForStudentQuery, GetScheduledLessonsForStudentQueryPayload>
{
    private readonly ILessonQueryModelRepository lessonQueryModelRepository;

    public GetScheduledLessonsForStudentQueryHandler(ILessonQueryModelRepository lessonQueryModelRepository) => this.lessonQueryModelRepository = lessonQueryModelRepository;

    public async Task<Result<GetScheduledLessonsForStudentQueryPayload>> Handle(GetScheduledLessonsForStudentQuery query, CancellationToken cancellationToken)
    {
        var scheduledLesson = await lessonQueryModelRepository.GetScheduledLessonsForStudent(query, cancellationToken);
        var payload = new GetScheduledLessonsForStudentQueryPayload(scheduledLesson);

        return Result.Ok(payload);
    }
}
