using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;

public sealed class Subject : Enumeration
{
    private Subject(int value, string name) : base(value, name) { }

    public static readonly Subject Math = new(0, "Математика");

    public static readonly Subject Bulgarian = new(1, "Български език");

    public static readonly Subject Literature = new(2, "Литература");

    public static readonly Subject BulgarianAndLiterature = new(3, "Български език и литература");
}
