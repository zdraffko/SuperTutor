namespace SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountAddressInformation;

public class UpdateAccountAddressInformationRequest
{
    public UpdateAccountAddressInformationRequest(
    string connectedAccountId,
    string connectedPersonId,
    string state,
    string city,
    string addressLineOne,
    string addressLineTwo,
    int postalCode)
    {
        ConnectedAccountId = connectedAccountId;
        ConnectedPersonId = connectedPersonId;
        State = state;
        City = city;
        AddressLineOne = addressLineOne;
        AddressLineTwo = addressLineTwo;
        PostalCode = postalCode;
    }

    public string ConnectedAccountId { get; }

    public string ConnectedPersonId { get; }

    public string State { get; }

    public string City { get; }

    public string AddressLineOne { get; }

    public string AddressLineTwo { get; }

    public int PostalCode { get; }
}
