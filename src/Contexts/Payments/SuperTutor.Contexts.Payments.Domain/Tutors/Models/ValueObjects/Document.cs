namespace SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

public class Document
{
    public Document(string externalId, string name, string url)
    {
        ExternalId = externalId;
        Name = name;
        Url = url;
    }

    public string ExternalId { get; }

    public string Name { get; }

    public string Url { get; }
}
