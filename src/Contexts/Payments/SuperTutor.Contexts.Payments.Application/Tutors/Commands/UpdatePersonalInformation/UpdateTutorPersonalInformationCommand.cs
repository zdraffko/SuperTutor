using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdatePersonalInformation;

public class UpdateTutorPersonalInformationCommand : Command
{
    public UpdateTutorPersonalInformationCommand(TutorId tutorId, string firstName, string lastName, DateOnly dateOfBirth)
    {
        TutorId = tutorId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public TutorId TutorId { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public DateOnly DateOfBirth { get; }
}
