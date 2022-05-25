using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Payments.Application.Tutors.IntegrationEvents.Identity.Users.TutorRegistered;

public class CreateTutorCommand : Command
{
    public CreateTutorCommand(UserId userId, string email)
    {
        UserId = userId;
        Email = email;
    }

    public UserId UserId { get; }

    public string Email { get; }
}
