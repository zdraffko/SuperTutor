namespace SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

public class TermsOfService
{
    public TermsOfService(string type, string ipOfAcceptance)
    {
        Type = type;
        DateOfAcceptance = DateTime.UtcNow;
        IpOfAcceptance = ipOfAcceptance;
    }

    public string Type { get; }

    public DateTime DateOfAcceptance { get; }

    public string IpOfAcceptance { get; }
}
