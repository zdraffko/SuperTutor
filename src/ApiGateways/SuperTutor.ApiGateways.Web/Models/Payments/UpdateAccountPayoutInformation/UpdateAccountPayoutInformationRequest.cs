namespace SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPayoutInformation;

public class UpdateAccountPayoutInformationRequest
{
    public UpdateAccountPayoutInformationRequest(
    string connectedAccountId,
    string bankAccountHolderFullName,
    string bankAccountHolderType,
    string bankAccountIban)
    {
        ConnectedAccountId = connectedAccountId;
        BankAccountHolderFullName = bankAccountHolderFullName;
        BankAccountHolderType = bankAccountHolderType;
        BankAccountIban = bankAccountIban;
    }

    public string ConnectedAccountId { get; }

    public string BankAccountHolderFullName { get; }

    public string BankAccountHolderType { get; }

    public string BankAccountIban { get; }
}
