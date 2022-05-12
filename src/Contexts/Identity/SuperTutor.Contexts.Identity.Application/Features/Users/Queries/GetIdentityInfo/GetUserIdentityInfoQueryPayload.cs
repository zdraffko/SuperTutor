using SuperTutor.Contexts.Identity.Domain.Users.Models.Enumerations;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Queries.GetIdentityInfo;

public class GetUserIdentityInfoQueryPayload
{
    public GetUserIdentityInfoQueryPayload(string userEmail, UserType userType)
    {
        UserEmail = userEmail;
        UserType = userType;
    }

    public string UserEmail { get; }

    public UserType UserType { get; }
}
