namespace SuperTutor.ApiGateways.Web.Models.Identity.GetIdentityInfo;

public class GetIdentityInfoRequest
{
    public GetIdentityInfoRequest(string userId) => UserId = userId;

    public string UserId { get; }
}
