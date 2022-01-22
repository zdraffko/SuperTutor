using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;

public sealed class Subject : Enumeration
{
    private Subject(int value, string name) : base(value, name) { }

    public static readonly Subject Math = new(0, nameof(Math));

    public static readonly Subject Bulgarian = new(1, nameof(Bulgarian));

    public static readonly Subject Literature = new(2, nameof(Literature));

    public static readonly Subject BulgarianAndLiterature = new(3, nameof(BulgarianAndLiterature));
}
