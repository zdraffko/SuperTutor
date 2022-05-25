using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

public class BankAccount : ValueObject
{
    public BankAccount(string holderFullName, string holderType, string iban)
    {
        HolderFullName = holderFullName;
        HolderType = holderType;
        Iban = iban;
    }

    public string HolderFullName { get; }

    public string HolderType { get; }

    public string Iban { get; }
}
