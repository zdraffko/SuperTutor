using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.Create;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Api.Controllers;

public  class StudentProfilesController : ApiController
{
    private readonly ICommandHandler<CreateStudentProfileCommand> createStudentProfileCommandHandler;

    public StudentProfilesController(ICommandHandler<CreateStudentProfileCommand> createStudentProfileCommandHandler)
    {
        this.createStudentProfileCommandHandler = createStudentProfileCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateStudentProfileCommand command, CancellationToken cancellationToken)
        => await Handle(createStudentProfileCommandHandler, command, cancellationToken);
}
