using FluentResults;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.Register;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IUserService userService;

    public RegisterUserCommandHandler(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        => await userService.Register(command.Email, command.Username, command.Password);
}
