using FluentResults;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForTutor;

internal class GetActiveClassroomForTutorQueryHandler : IQueryHandler<GetActiveClassroomForTutorQuery, GetActiveClassroomForTutorQueryPayload>
{
    private readonly IClassroomQueryRepository classroomQueryRepository;

    public GetActiveClassroomForTutorQueryHandler(IClassroomQueryRepository classroomQueryRepository) => this.classroomQueryRepository = classroomQueryRepository;

    public async Task<Result<GetActiveClassroomForTutorQueryPayload>> Handle(GetActiveClassroomForTutorQuery query, CancellationToken cancellationToken)
    {
        var queryPayload = await classroomQueryRepository.GetActiveForTutor(query.TutorId, cancellationToken);

        return Result.Ok(queryPayload);
    }
}
