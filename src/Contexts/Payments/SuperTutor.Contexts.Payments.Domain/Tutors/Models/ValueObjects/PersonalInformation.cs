using SuperTutor.SharedLibraries.BuildingBlocks.Domain.ValueObjects;

namespace SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

public class PersonalInformation : ValueObject
{
    public PersonalInformation(string firstName, string lastName, int dateOfBirthDay, int dateOfBirthMonth, int dateOfBirthYear)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirthDay = dateOfBirthDay;
        DateOfBirthMonth = dateOfBirthMonth;
        DateOfBirthYear = dateOfBirthYear;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public int DateOfBirthDay { get; }

    public int DateOfBirthMonth { get; }

    public int DateOfBirthYear { get; }
}
