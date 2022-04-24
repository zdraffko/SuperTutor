using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects;

namespace SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Models.ValueObjects;

public class TutoringGrade : ValueObject
{
    public TutoringGrade(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public int Value { get; }
    public string Name { get; }
}
