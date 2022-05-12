namespace SuperTutor.Contexts.Identity.Infrastructure.Tokens.Options;

public class AuthTokenOptions
{
    public const string SectionName = "AuthToken";

    public string SecretKey { get; set; } = string.Empty;
}
