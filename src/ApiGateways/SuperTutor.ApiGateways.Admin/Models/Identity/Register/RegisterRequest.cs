namespace SuperTutor.ApiGateways.Web.Models.Identity.Register;

public class RegisterRequest
{
    public RegisterRequest(string email, string password, string firstName, string lastName)
    {
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Email { get; }

    public string Password { get; }

    public string FirstName { get; }

    public string LastName { get; }
}
