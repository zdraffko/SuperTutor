using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Delete;

public class DeleteUserCommand : Command
{
    public DeleteUserCommand(string email)
    {
        Email = email;
    }

    public string Email { get; }
}
