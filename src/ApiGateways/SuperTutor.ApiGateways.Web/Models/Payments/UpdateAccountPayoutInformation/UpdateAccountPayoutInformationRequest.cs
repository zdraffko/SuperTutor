namespace SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPayoutInformation;

public class UpdateAccountPayoutInformationRequest
{
    public UpdateAccountPayoutInformationRequest(string bankAccountHolderFullName, string bankAccountHolderType, string bankAccountIban)
    {
        BankAccountHolderFullName = bankAccountHolderFullName;
        BankAccountHolderType = bankAccountHolderType;
        BankAccountIban = bankAccountIban;
    }

    public string BankAccountHolderFullName { get; }

    public string BankAccountHolderType { get; }

    public string BankAccountIban { get; }
}
