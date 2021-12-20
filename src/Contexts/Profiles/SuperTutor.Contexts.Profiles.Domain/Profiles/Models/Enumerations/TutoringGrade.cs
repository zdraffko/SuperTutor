using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;

public class TutoringGrade : Enumeration
{
    private TutoringGrade(int value, string name) : base(value, name) { }

    public static readonly TutoringGrade First = new(1, nameof(First));

    public static readonly TutoringGrade Second = new(2, nameof(Second));

    public static readonly TutoringGrade Third = new(3, nameof(Third));

    public static readonly TutoringGrade Forth = new(4, nameof(Forth));

    public static readonly TutoringGrade Fifth = new(5, nameof(Fifth));

    public static readonly TutoringGrade Sixth = new(6, nameof(Sixth));

    public static readonly TutoringGrade Seventh = new(7, nameof(Seventh));

    public static readonly TutoringGrade Eighth = new(8, nameof(Eighth));

    public static readonly TutoringGrade Ninth = new(9, nameof(Ninth));

    public static readonly TutoringGrade Tenth = new(10, nameof(Tenth));

    public static readonly TutoringGrade Eleventh = new(11, nameof(Eleventh));

    public static readonly TutoringGrade Twelveth = new(12, nameof(Twelveth));
}
