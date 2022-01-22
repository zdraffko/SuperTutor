using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;

public sealed class Grade : Enumeration
{
    private Grade(int value, string name) : base(value, name) { }

    public static readonly Grade First = new(1, nameof(First));

    public static readonly Grade Second = new(2, nameof(Second));

    public static readonly Grade Third = new(3, nameof(Third));

    public static readonly Grade Forth = new(4, nameof(Forth));

    public static readonly Grade Fifth = new(5, nameof(Fifth));

    public static readonly Grade Sixth = new(6, nameof(Sixth));

    public static readonly Grade Seventh = new(7, nameof(Seventh));

    public static readonly Grade Eighth = new(8, nameof(Eighth));

    public static readonly Grade Ninth = new(9, nameof(Ninth));

    public static readonly Grade Tenth = new(10, nameof(Tenth));

    public static readonly Grade Eleventh = new(11, nameof(Eleventh));

    public static readonly Grade Twelveth = new(12, nameof(Twelveth));
}
