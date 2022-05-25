using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Invariants;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Invariants;

public class TutorExternalPaymentAccountCanOnlyBeCreatedOnceInvariant : Invariant
{
    private readonly ExternalPaymentAccount? externalPaymentAccount;
    public TutorExternalPaymentAccountCanOnlyBeCreatedOnceInvariant(ExternalPaymentAccount? externalPaymentAccount)
        : base("The tutor already has an  external payment account") => this.externalPaymentAccount = externalPaymentAccount;

    public override bool IsValid() => externalPaymentAccount is null;
}
