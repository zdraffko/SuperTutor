using SuperTutor.Contexts.Payments.Domain.Charges.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Charges.Events;

public class ChargeCompletedDomainEvent : DomainEvent
{
    public ChargeCompletedDomainEvent(ChargeId chargeId, ChargeStatus status)
    {
        ChargeId = chargeId;
        Status = status;
    }

    public ChargeId ChargeId { get; }

    public ChargeStatus Status { get; }
}
