namespace SuperTutor.ApiGateways.Web.Models.Payments.UpdateAccountPersonalInformation;

public class UpdateAccountPersonalInformationRequest
{
    public UpdateAccountPersonalInformationRequest(DateOnly dateOfBirth) => DateOfBirth = dateOfBirth;

    public DateOnly DateOfBirth { get; }
}
