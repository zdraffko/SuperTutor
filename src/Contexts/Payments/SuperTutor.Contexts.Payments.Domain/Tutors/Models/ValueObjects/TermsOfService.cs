namespace SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

public class TermsOfService
{
    public TermsOfService(string type, DateTime dateOfAcceptance, string ipOfAcceptance)
    {
        Type = type;
        DateOfAcceptance = dateOfAcceptance;
        IpOfAcceptance = ipOfAcceptance;
    }

    public string Type { get; }

    public DateTime DateOfAcceptance { get; }

    public string IpOfAcceptance { get; }
}
