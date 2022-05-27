using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

public class PersonalInformation : ValueObject
{
    public PersonalInformation(int dateOfBirthDay, int dateOfBirthMonth, int dateOfBirthYear)
    {
        DateOfBirthDay = dateOfBirthDay;
        DateOfBirthMonth = dateOfBirthMonth;
        DateOfBirthYear = dateOfBirthYear;
    }

    public int DateOfBirthDay { get; }

    public int DateOfBirthMonth { get; }

    public int DateOfBirthYear { get; }
}
