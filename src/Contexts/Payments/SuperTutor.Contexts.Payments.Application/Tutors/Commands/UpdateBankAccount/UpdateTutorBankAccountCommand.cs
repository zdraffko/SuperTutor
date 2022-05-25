using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdateBankAccount;

public class UpdateTutorBankAccountCommand : Command
{
    public UpdateTutorBankAccountCommand(TutorId tutorId, string bankAccountHolderFullName, string bankAccountHolderType, string bankAccountIban)
    {
        TutorId = tutorId;
        BankAccountHolderFullName = bankAccountHolderFullName;
        BankAccountHolderType = bankAccountHolderType;
        BankAccountIban = bankAccountIban;
    }

    public TutorId TutorId { get; }

    public string BankAccountHolderFullName { get; }

    public string BankAccountHolderType { get; }

    public string BankAccountIban { get; }
}
