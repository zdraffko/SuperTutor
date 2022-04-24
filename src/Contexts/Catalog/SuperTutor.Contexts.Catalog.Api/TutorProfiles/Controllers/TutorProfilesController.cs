using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Catalog.Application.TutorProfiles.Queries.GetByFilter;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Catalog.Api.TutorProfiles.Controllers;

public class TutorProfilesController : ApiController
{
    private readonly IQueryHandler<GetTutorProfilesByFilterQuery, GetTutorProfilesByFilterQueryPayload> getTutorProfilesByFilterQueryHandler;

    public TutorProfilesController(IQueryHandler<GetTutorProfilesByFilterQuery, GetTutorProfilesByFilterQueryPayload> getTutorProfilesByFilterQueryHandler) => this.getTutorProfilesByFilterQueryHandler = getTutorProfilesByFilterQueryHandler;

    [HttpGet]
    public async Task<ActionResult<GetTutorProfilesByFilterQueryPayload>> GetByFilter([FromJsonQuery] GetTutorProfilesByFilterQuery query, CancellationToken cancellationToken)
        => await Handle(getTutorProfilesByFilterQueryHandler, query, cancellationToken);
}
