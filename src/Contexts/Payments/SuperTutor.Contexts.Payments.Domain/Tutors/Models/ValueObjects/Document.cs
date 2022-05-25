namespace SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

public class Document
{
    public Document(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public string Name { get; }

    public string Url { get; }
}
