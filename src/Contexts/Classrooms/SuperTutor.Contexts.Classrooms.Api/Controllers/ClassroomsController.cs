using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForStudent;
using SuperTutor.Contexts.Classrooms.Application.Classrooms.Queries.GetActiveForTutor;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Classrooms.Api.Controllers;

public class ClassroomsController : ApiController
{
    private readonly IQueryHandler<GetActiveClassroomForTutorQuery, GetActiveClassroomForTutorQueryPayload> getActiveClassroomForTutorQueryHandler;
    private readonly IQueryHandler<GetActiveClassroomForStudentQuery, GetActiveClassroomForStudentQueryPayload> getActiveClassroomForStudentQueryHandler;

    public ClassroomsController(IQueryHandler<GetActiveClassroomForTutorQuery, GetActiveClassroomForTutorQueryPayload> getActiveClassroomForTutorQueryHandler, IQueryHandler<GetActiveClassroomForStudentQuery, GetActiveClassroomForStudentQueryPayload> getActiveClassroomForStudentQueryHandler)
    {
        this.getActiveClassroomForTutorQueryHandler = getActiveClassroomForTutorQueryHandler;
        this.getActiveClassroomForStudentQueryHandler = getActiveClassroomForStudentQueryHandler;
    }

    [HttpGet]
    public async Task<ActionResult<GetActiveClassroomForTutorQueryPayload>> GetActiveForTutor([FromJsonQuery] GetActiveClassroomForTutorQuery query, CancellationToken cancellationToken)
        => await Handle(getActiveClassroomForTutorQueryHandler, query, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetActiveClassroomForStudentQueryPayload>> GetActiveForStudent([FromJsonQuery] GetActiveClassroomForStudentQuery query, CancellationToken cancellationToken)
        => await Handle(getActiveClassroomForStudentQueryHandler, query, cancellationToken);
}
