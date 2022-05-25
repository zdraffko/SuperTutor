using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdatePersonalInformation;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Api.Tutors.Controllers;

public class TutorsController : ApiController
{
    private readonly ICommandHandler<UpdateTutorPersonalInformationCommand> updateTutorPersonalInformationCommandHandler;

    public TutorsController(ICommandHandler<UpdateTutorPersonalInformationCommand> updateTutorPersonalInformationCommandHandler) => this.updateTutorPersonalInformationCommandHandler = updateTutorPersonalInformationCommandHandler;

    [HttpPost]
    public async Task<ActionResult> UpdatePersonalInformation(UpdateTutorPersonalInformationCommand command, CancellationToken cancellationToken)
        => await Handle(updateTutorPersonalInformationCommandHandler, command, cancellationToken);
}
