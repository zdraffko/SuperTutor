using SuperTutor.Contexts.Identity.Domain.Users.Models.Enumerations;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Queries.GetIdentityInfo;

public class GetUserIdentityInfoQueryPayload
{
    public GetUserIdentityInfoQueryPayload(string userEmail, UserType userType, string firstName, string lastName)
    {
        UserEmail = userEmail;
        UserType = userType;
        FirstName = firstName;
        LastName = lastName;
    }

    public string UserEmail { get; }

    public UserType UserType { get; }

    public string FirstName { get; }

    public string LastName { get; }
}
