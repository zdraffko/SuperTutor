using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Catalog.Application.Students.Commands.AddFavoriteFilter;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Api.Students.Controllers;

public class StudentsController : ApiController
{
    private readonly ICommandHandler<AddFavoriteFilterForStudentCommand> addFavoriteFilterForStudentCommandHandler;

    public StudentsController(ICommandHandler<AddFavoriteFilterForStudentCommand> addFavoriteFilterCommandHandler) => addFavoriteFilterForStudentCommandHandler = addFavoriteFilterCommandHandler;

    [HttpPost]
    public async Task<ActionResult> AddFavoriteFilter(AddFavoriteFilterForStudentCommand command, CancellationToken cancellationToken)
        => await Handle(addFavoriteFilterForStudentCommandHandler, command, cancellationToken);
}
