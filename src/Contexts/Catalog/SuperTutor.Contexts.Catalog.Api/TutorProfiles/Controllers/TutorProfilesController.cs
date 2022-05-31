using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;
using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetById;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Catalog.Api.TutorProfiles.Controllers;

public class TutorProfilesController : ApiController
{
    private readonly IQueryHandler<GetTutorProfilesByFilterQuery, GetTutorProfilesByFilterQueryPayload> getTutorProfilesByFilterQueryHandler;
    private readonly IQueryHandler<GetTutorProfileByIdQuery, GetTutorProfileByIdQueryPayload> getTutorProfileByIdQueryHandler;

    public TutorProfilesController(IQueryHandler<GetTutorProfilesByFilterQuery, GetTutorProfilesByFilterQueryPayload> getTutorProfilesByFilterQueryHandler, IQueryHandler<GetTutorProfileByIdQuery, GetTutorProfileByIdQueryPayload> getTutorProfileByIdQueryHandler)
    {
        this.getTutorProfilesByFilterQueryHandler = getTutorProfilesByFilterQueryHandler;
        this.getTutorProfileByIdQueryHandler = getTutorProfileByIdQueryHandler;
    }

    [HttpGet]
    public async Task<ActionResult<GetTutorProfilesByFilterQueryPayload>> GetByFilter([FromJsonQuery] GetTutorProfilesByFilterQuery query, CancellationToken cancellationToken)
        => await Handle(getTutorProfilesByFilterQueryHandler, query, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetTutorProfileByIdQueryPayload>> GetById([FromJsonQuery] GetTutorProfileByIdQuery query, CancellationToken cancellationToken)
        => await Handle(getTutorProfileByIdQueryHandler, query, cancellationToken);
}
