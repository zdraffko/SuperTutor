using FluentResults;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetStudentConnectionId;

internal class GetClassroomStudentConnectionIdQueryHandler : IQueryHandler<GetClassroomStudentConnectionIdQuery, GetClassroomStudentConnectionIdQueryPayload>
{
    private readonly IClassroomQueryRepository classroomQueryRepository;

    public GetClassroomStudentConnectionIdQueryHandler(IClassroomQueryRepository classroomQueryRepository) => this.classroomQueryRepository = classroomQueryRepository;

    public async Task<Result<GetClassroomStudentConnectionIdQueryPayload>> Handle(GetClassroomStudentConnectionIdQuery query, CancellationToken cancellationToken)
    {
        var studentConnectionId = await classroomQueryRepository.GetStudentConnectionId(query.ClassroomName);
        var payload = new GetClassroomStudentConnectionIdQueryPayload(studentConnectionId);

        return Result.Ok(payload);
    }
}
