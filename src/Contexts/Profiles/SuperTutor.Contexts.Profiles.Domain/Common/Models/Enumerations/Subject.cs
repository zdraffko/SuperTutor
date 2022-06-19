using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;

public sealed class Subject : Enumeration
{
    private Subject(int value, string name) : base(value, name) { }

    public static readonly Subject Math = new(0, "Математика");

    public static readonly Subject Bulgarian = new(1, "Български език");

    public static readonly Subject Literature = new(2, "Литература");

    public static readonly Subject BulgarianAndLiterature = new(3, "Български език и литература");

    public static readonly Subject InformationTechnology = new(4, "Информационни технологии");

    public static readonly Subject Geography = new(5, "География");

    public static readonly Subject History = new(6, "История");

    public static readonly Subject Philosophy = new(7, "Философия");

    public static readonly Subject Biology = new(8, "Биология");

    public static readonly Subject Physics = new(9, "Физика");

    public static readonly Subject Chemistry = new(10, "Химия");

    public static readonly Subject Music = new(11, "Музика");

    public static readonly Subject Art = new(12, "Изобразително изкуство");

    public static readonly Subject PhysicalEducationAndSport = new(13, "Физическо възпитание и спорт");
}
