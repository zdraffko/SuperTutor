using FluentResults;
using SuperTutor.Contexts.Payments.Application.Charges.Commands.Complete;

namespace SuperTutor.Contexts.Payments.Application.Charges.Shared;

public interface IChargesWebhookEventParser
{
    Result<CompleteChargeCommand> ParseForCompleteChargeCommand(string rawEvent, string signature);
}
