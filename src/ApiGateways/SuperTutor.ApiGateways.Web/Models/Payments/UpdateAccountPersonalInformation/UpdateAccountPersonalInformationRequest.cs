namespace SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPersonalInformation;

public class UpdateAccountPersonalInformationRequest
{
    public UpdateAccountPersonalInformationRequest(string tutorId, string firstName, string lastName, DateOnly dateOfBirth)
    {
        TutorId = tutorId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public string TutorId { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public DateOnly DateOfBirth { get; }
}
