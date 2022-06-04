using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Charges.Commands.Complete;

public class CompleteChargeCommand : Command
{
    public CompleteChargeCommand(ChargeId chargeId) => ChargeId = chargeId;

    public ChargeId ChargeId { get; }
}
