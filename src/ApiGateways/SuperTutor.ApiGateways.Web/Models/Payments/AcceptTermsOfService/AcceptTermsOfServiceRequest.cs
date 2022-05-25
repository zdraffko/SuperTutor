namespace SuperTutor.ApiGateways.Web.Models.Payments.AcceptTermsOfService;

public class AcceptTermsOfServiceRequest
{
    public AcceptTermsOfServiceRequest(string userId) => UserId = userId;

    public string UserId { get; }
}
