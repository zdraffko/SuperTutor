namespace SuperTutor.ApiGateways.Web.Models.Identity.Register;

public class RegisterRequest
{
    public RegisterRequest(string email, string password, UserType type, string firstName, string lastName)
    {
        Email = email;
        Password = password;
        Type = type;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Email { get; }

    public string Password { get; }

    public UserType Type { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public enum UserType
    {
        Tutor = 1,
        Student = 2
    }
}
