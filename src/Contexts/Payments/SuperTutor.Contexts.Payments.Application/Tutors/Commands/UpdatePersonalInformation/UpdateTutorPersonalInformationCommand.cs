using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdatePersonalInformation;

public class UpdateTutorPersonalInformationCommand : Command
{
    public UpdateTutorPersonalInformationCommand(TutorId tutorId, DateOnly dateOfBirth)
    {
        TutorId = tutorId;
        DateOfBirth = dateOfBirth;
    }

    public TutorId TutorId { get; }

    public DateOnly DateOfBirth { get; }
}
