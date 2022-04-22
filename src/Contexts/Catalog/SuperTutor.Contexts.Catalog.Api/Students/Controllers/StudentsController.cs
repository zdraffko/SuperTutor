using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Catalog.Application.Students.Commands.AddFavoriteFilter;
using SuperTutor.Contexts.Catalog.Application.Students.Commands.RemoveFavoriteFilter;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Api.Students.Controllers;

public class StudentsController : ApiController
{
    private readonly ICommandHandler<AddFavoriteFilterForStudentCommand> addFavoriteFilterForStudentCommandHandler;
    private readonly ICommandHandler<RemoveFavoriteFilterForStudentCommand> removeFavoriteFilterForStudentCommandHandler;

    public StudentsController(
        ICommandHandler<AddFavoriteFilterForStudentCommand> addFavoriteFilterForStudentCommandHandler,
        ICommandHandler<RemoveFavoriteFilterForStudentCommand> removeFavoriteFilterForStudentCommandHandler)
    {
        this.addFavoriteFilterForStudentCommandHandler = addFavoriteFilterForStudentCommandHandler;
        this.removeFavoriteFilterForStudentCommandHandler = removeFavoriteFilterForStudentCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult> AddFavoriteFilter(AddFavoriteFilterForStudentCommand command, CancellationToken cancellationToken)
        => await Handle(addFavoriteFilterForStudentCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RemoveFavoriteFilter(RemoveFavoriteFilterForStudentCommand command, CancellationToken cancellationToken)
        => await Handle(removeFavoriteFilterForStudentCommandHandler, command, cancellationToken);
}
