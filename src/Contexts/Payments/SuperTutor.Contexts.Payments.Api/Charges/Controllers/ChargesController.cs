using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Payments.Application.Charges.Commands.Create;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Api.Charges.Controllers;

public class ChargesController : ApiController
{
    private readonly ICommandHandler<CreateChargeCommand, CreateChargeCommandPayload> createChargeCommandHandler;

    public ChargesController(ICommandHandler<CreateChargeCommand, CreateChargeCommandPayload> createChargeCommandHandler) => this.createChargeCommandHandler = createChargeCommandHandler;

    [HttpPost]
    public async Task<ActionResult<CreateChargeCommandPayload>> Create(CreateChargeCommand command, CancellationToken cancellationToken)
        => await Handle(createChargeCommandHandler, command, cancellationToken);
}
