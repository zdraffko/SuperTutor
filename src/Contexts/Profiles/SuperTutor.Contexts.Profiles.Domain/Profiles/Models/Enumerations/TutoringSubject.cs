using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;

public sealed class TutoringSubject : Enumeration
{
    private TutoringSubject(int value, string name) : base(value, name) { }

    public static readonly TutoringSubject Math = new(0, nameof(Math));

    public static readonly TutoringSubject Bulgarian = new(1, nameof(Bulgarian));

    public static readonly TutoringSubject Literature = new(2, nameof(Literature));

    public static readonly TutoringSubject BulgarianAndLiterature = new(3, nameof(BulgarianAndLiterature));
}
