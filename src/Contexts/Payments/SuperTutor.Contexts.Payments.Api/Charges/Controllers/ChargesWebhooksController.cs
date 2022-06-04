using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Payments.Application.Charges.Commands.Complete;
using SuperTutor.Contexts.Payments.Application.Charges.Shared;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Extensions;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Api.Charges.Controllers;

public class ChargesWebhooksController : ApiController
{
    private readonly IChargesWebhookEventParser chargesWebhookEventParser;
    private readonly ICommandHandler<CompleteChargeCommand> completeChargeCommandHandler;

    public ChargesWebhooksController(IChargesWebhookEventParser chargesWebhookEventParser, ICommandHandler<CompleteChargeCommand> completeChargeCommandHandler)
    {
        this.chargesWebhookEventParser = chargesWebhookEventParser;
        this.completeChargeCommandHandler = completeChargeCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult> Succeeded(CancellationToken cancellationToken)
    {
        var rawEvent = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var signature = Request.Headers["Stripe-Signature"];

        var commandParseResult = chargesWebhookEventParser.ParseForCompleteChargeCommand(rawEvent, signature);
        if (commandParseResult.IsFailed)
        {
            return BadRequest(commandParseResult.Errors.FirstOrDefault()?.Message);
        }

        var completeChargeCommandResult = await completeChargeCommandHandler.Handle(commandParseResult.Value, cancellationToken);

        return completeChargeCommandResult.ToActionResult();
    }
}

