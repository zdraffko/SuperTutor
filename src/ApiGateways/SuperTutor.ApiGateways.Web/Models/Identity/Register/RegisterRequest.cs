namespace SuperTutor.ApiGateways.Web.Models.Identity.Register;

public class RegisterRequest
{
    public RegisterRequest(string email, string password, UserType type)
    {
        Email = email;
        Password = password;
        Type = type;
    }

    public string Email { get; }

    public string Password { get; }

    public UserType Type { get; }

    public enum UserType
    {
        Tutor = 1,
        Student = 2
    }
}
