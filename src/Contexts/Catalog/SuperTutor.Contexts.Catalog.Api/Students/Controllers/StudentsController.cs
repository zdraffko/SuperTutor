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
    private readonly IQueryHandler<GetAllFavoriteFiltersForStudentQuery, GetAllFavoriteFiltersForStudentQueryPayload> getAllFavoriteFiltersForStudentQueryHandler;

    public StudentsController(
        ICommandHandler<AddFavoriteFilterForStudentCommand> addFavoriteFilterForStudentCommandHandler,
        ICommandHandler<RemoveFavoriteFilterForStudentCommand> removeFavoriteFilterForStudentCommandHandler,
        IQueryHandler<GetAllFavoriteFiltersForStudentQuery, GetAllFavoriteFiltersForStudentQueryPayload> getAllFavoriteFiltersForStudentQueryHandler)
    {
        this.addFavoriteFilterForStudentCommandHandler = addFavoriteFilterForStudentCommandHandler;
        this.removeFavoriteFilterForStudentCommandHandler = removeFavoriteFilterForStudentCommandHandler;
        this.getAllFavoriteFiltersForStudentQueryHandler = getAllFavoriteFiltersForStudentQueryHandler;
    }

    [HttpPost]
    public async Task<ActionResult> AddFavoriteFilter(AddFavoriteFilterForStudentCommand command, CancellationToken cancellationToken)
        => await Handle(addFavoriteFilterForStudentCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RemoveFavoriteFilter(RemoveFavoriteFilterForStudentCommand command, CancellationToken cancellationToken)
        => await Handle(removeFavoriteFilterForStudentCommandHandler, command, cancellationToken);

    [HttpGet]
    public async Task<ActionResult<GetAllFavoriteFiltersForStudentQueryPayload>> GetAllFavoriteFilters([FromJsonQuery] GetAllFavoriteFiltersForStudentQuery query, CancellationToken cancellationToken)
        => await Handle(getAllFavoriteFiltersForStudentQueryHandler, query, cancellationToken);
}
