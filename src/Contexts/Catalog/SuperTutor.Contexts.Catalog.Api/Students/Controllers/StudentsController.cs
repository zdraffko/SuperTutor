using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Catalog.Application.Students.Commands.AddFavoriteFilter;
using SuperTutor.Contexts.Catalog.Application.Students.Commands.RemoveFavoriteFilter;
using SuperTutor.Contexts.Catalog.Application.Students.Queries.GetAllfavoriteFilters;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Queries;

namespace SuperTutor.Contexts.Catalog.Api.Students.Controllers;

public class StudentsController : ApiController
{
    private readonly ICommandHandler<AddFavoriteFilterForStudentCommand> addFavoriteFilterForStudentCommandHandler;
    private readonly ICommandHandler<RemoveFavoriteFilterForStudentCommand> removeFavoriteFilterForStudentCommandHandler;
    private readonly IQueryHandler<GetAllfavoriteFiltersForStudentQuery, GetAllfavoriteFiltersForStudentQueryPayload> getAllfavoriteFiltersForStudentQueryHandler;

    public StudentsController(
        ICommandHandler<AddFavoriteFilterForStudentCommand> addFavoriteFilterForStudentCommandHandler,
        ICommandHandler<RemoveFavoriteFilterForStudentCommand> removeFavoriteFilterForStudentCommandHandler,
        IQueryHandler<GetAllfavoriteFiltersForStudentQuery, GetAllfavoriteFiltersForStudentQueryPayload> getAllfavoriteFiltersForStudentQueryHandler)
    {
        this.addFavoriteFilterForStudentCommandHandler = addFavoriteFilterForStudentCommandHandler;
        this.removeFavoriteFilterForStudentCommandHandler = removeFavoriteFilterForStudentCommandHandler;
        this.getAllfavoriteFiltersForStudentQueryHandler = getAllfavoriteFiltersForStudentQueryHandler;
    }

    [HttpPost]
    public async Task<ActionResult> AddFavoriteFilter(AddFavoriteFilterForStudentCommand command, CancellationToken cancellationToken)
        => await Handle(addFavoriteFilterForStudentCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RemoveFavoriteFilter(RemoveFavoriteFilterForStudentCommand command, CancellationToken cancellationToken)
        => await Handle(removeFavoriteFilterForStudentCommandHandler, command, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetAllfavoriteFiltersForStudentQueryPayload>> GetAllFavoriteFilters([FromJsonQuery] GetAllfavoriteFiltersForStudentQuery query, CancellationToken cancellationToken)
        => await Handle(getAllfavoriteFiltersForStudentQueryHandler, query, cancellationToken);
}
