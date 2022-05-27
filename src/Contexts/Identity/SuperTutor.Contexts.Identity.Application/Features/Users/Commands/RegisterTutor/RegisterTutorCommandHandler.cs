using FluentResults;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.Contexts.Identity.IntegrationEvents.Users;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Identity.Application.Features.Users.Commands.RegisterTutor;

internal class RegisterTutorCommandHandler : ICommandHandler<RegisterTutorCommand, RegisterTutorCommandResult>
{
    private readonly IUserService userService;
    private readonly IIntegrationEventsService integrationEventsService;

    public RegisterTutorCommandHandler(IUserService userService, IIntegrationEventsService integrationEventsService)
    {
        this.userService = userService;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result<RegisterTutorCommandResult>> Handle(RegisterTutorCommand command, CancellationToken cancellationToken)
    {
        var registerResult = await userService.RegisterTutor(command.Email, command.Password, command.FirstName, command.LastName);
        if (registerResult.IsFailed)
        {
            return registerResult.ToResult<RegisterTutorCommandResult>();
        }

        integrationEventsService.Raise(new TutorRegisteredIntegrationEvent(registerResult.Value, command.Email, command.FirstName, command.LastName));

        var loginResult = await userService.Login(command.Email, command.Password);
        if (loginResult.IsFailed)
        {
            return loginResult.ToResult<RegisterTutorCommandResult>();
        }

        var commandResult = new RegisterTutorCommandResult(loginResult.Value);

        return Result.Ok(commandResult);
    }
}
