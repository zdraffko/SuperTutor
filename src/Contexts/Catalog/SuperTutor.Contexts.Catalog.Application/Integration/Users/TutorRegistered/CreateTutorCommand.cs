using SuperTutor.Contexts.Catalog.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Users.TutorRegistered;

public class CreateTutorCommand : Command
{
    public CreateTutorCommand(TutorId tutorId, string firstName, string lastName)
    {
        TutorId = tutorId;
        FirstName = firstName;
        LastName = lastName;
    }

    public TutorId TutorId { get; }

    public string FirstName { get; }

    public string LastName { get; }
}
