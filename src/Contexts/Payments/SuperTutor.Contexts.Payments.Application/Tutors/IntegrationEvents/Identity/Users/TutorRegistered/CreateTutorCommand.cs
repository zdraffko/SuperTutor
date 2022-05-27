using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Tutors.IntegrationEvents.Identity.Users.TutorRegistered;

public class CreateTutorCommand : Command
{
    public CreateTutorCommand(TutorId tutorId, string email, string firstName, string lastName)
    {
        TutorId = tutorId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public TutorId TutorId { get; }

    public string Email { get; }

    public string FirstName { get; }

    public string LastName { get; }
}
