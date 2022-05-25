using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdateAddress;
using SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdatePersonalInformation;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Api.Tutors.Controllers;

public class TutorsController : ApiController
{
    private readonly ICommandHandler<UpdateTutorPersonalInformationCommand> updateTutorPersonalInformationCommandHandler;
    private readonly ICommandHandler<UpdateTutorAddressCommand> updateTutorAddressCommandHandler;

    public TutorsController(ICommandHandler<UpdateTutorPersonalInformationCommand> updateTutorPersonalInformationCommandHandler, ICommandHandler<UpdateTutorAddressCommand> updateTutorAddressCommandHandler)
    {
        this.updateTutorPersonalInformationCommandHandler = updateTutorPersonalInformationCommandHandler;
        this.updateTutorAddressCommandHandler = updateTutorAddressCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult> UpdatePersonalInformation(UpdateTutorPersonalInformationCommand command, CancellationToken cancellationToken)
        => await Handle(updateTutorPersonalInformationCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> UpdateAddress(UpdateTutorAddressCommand command, CancellationToken cancellationToken)
        => await Handle(updateTutorAddressCommandHandler, command, cancellationToken);
}
