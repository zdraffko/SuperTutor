namespace SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPayoutInformation;

public class UpdateAccountPayoutInformationRequest
{
    public UpdateAccountPayoutInformationRequest(Guid tutorId, string bankAccountHolderFullName, string bankAccountHolderType, string bankAccountIban)
    {
        TutorId = tutorId;
        BankAccountHolderFullName = bankAccountHolderFullName;
        BankAccountHolderType = bankAccountHolderType;
        BankAccountIban = bankAccountIban;
    }

    public Guid TutorId { get; }

    public string BankAccountHolderFullName { get; }

    public string BankAccountHolderType { get; }

    public string BankAccountIban { get; }
}
