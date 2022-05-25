using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Events;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Events;

public class TutorAddressUpdatedDomainEvent : DomainEvent
{
    public TutorAddressUpdatedDomainEvent(TutorId tutorId, Address address, bool isAddressSyncedWithExternalPaymentAccount)
    {
        TutorId = tutorId;
        Address = address;
        IsAddressSyncedWithExternalPaymentAccount = isAddressSyncedWithExternalPaymentAccount;
    }

    public TutorId TutorId { get; }

    public Address Address { get; }

    public bool IsAddressSyncedWithExternalPaymentAccount { get; }
}
