namespace SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPersonalInformation;

public class UpdateAccountPersonalInformationRequest
{
    public UpdateAccountPersonalInformationRequest(string connectedAccountId, string connectedPersonId, string firstName, string lastName, DateOnly dateOfBirth)
    {
        ConnectedAccountId = connectedAccountId;
        ConnectedPersonId = connectedPersonId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public string ConnectedAccountId { get; }

    public string ConnectedPersonId { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public DateOnly DateOfBirth { get; }
}
