using FluentResults;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForStudent;

internal class GetActiveClassroomForStudentQueryHandler : IQueryHandler<GetActiveClassroomForStudentQuery, GetActiveClassroomForStudentQueryPayload>
{
    private readonly IClassroomQueryRepository classroomQueryRepository;

    public GetActiveClassroomForStudentQueryHandler(IClassroomQueryRepository classroomQueryRepository) => this.classroomQueryRepository = classroomQueryRepository;

    public async Task<Result<GetActiveClassroomForStudentQueryPayload>> Handle(GetActiveClassroomForStudentQuery query, CancellationToken cancellationToken)
    {
        var queryPayload = await classroomQueryRepository.GetActiveForStudent(query.StudentId, cancellationToken);

        return Result.Ok(queryPayload);
    }
}
