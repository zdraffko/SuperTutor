namespace SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPersonalInformation;

public class UpdateAccountPersonalInformationRequest
{
    public UpdateAccountPersonalInformationRequest(string firstName, string lastName, DateOnly dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public DateOnly DateOfBirth { get; }
}
