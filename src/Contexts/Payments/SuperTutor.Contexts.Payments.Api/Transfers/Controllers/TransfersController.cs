using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Payments.Application.Transfers.Queries.GetForTutor;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Payments.Api.Transfers.Controllers;

public class TransfersController : ApiController
{
    private readonly IQueryHandler<GetTransfersForTutorQuery, GetTransfersForTutorQueryPayload> getTransfersForTutorQueryHandler;

    public TransfersController(IQueryHandler<GetTransfersForTutorQuery, GetTransfersForTutorQueryPayload> getTransfersForTutorQueryHandler) => this.getTransfersForTutorQueryHandler = getTransfersForTutorQueryHandler;

    [HttpGet]
    public async Task<ActionResult<GetTransfersForTutorQueryPayload>> GetForTutor([FromJsonQuery] GetTransfersForTutorQuery query, CancellationToken cancellationToken)
        => await Handle(getTransfersForTutorQueryHandler, query, cancellationToken);
}
