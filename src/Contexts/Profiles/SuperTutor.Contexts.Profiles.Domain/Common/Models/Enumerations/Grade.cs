using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;

public sealed class Grade : Enumeration
{
    private Grade(int value, string name) : base(value, name) { }

    public static readonly Grade First = new(1, "Първи");

    public static readonly Grade Second = new(2, "Втори");

    public static readonly Grade Third = new(3, "Трети");

    public static readonly Grade Forth = new(4, "Четвърти");

    public static readonly Grade Fifth = new(5, "Пети");

    public static readonly Grade Sixth = new(6, "Шести");

    public static readonly Grade Seventh = new(7, "Седми");

    public static readonly Grade Eighth = new(8, "Осми");

    public static readonly Grade Ninth = new(9, "Девети");

    public static readonly Grade Tenth = new(10, "Десети");

    public static readonly Grade Eleventh = new(11, "Единадесети");

    public static readonly Grade Twelveth = new(12, "Дванадесети");
}
