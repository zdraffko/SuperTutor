using FluentResults;
using Microsoft.AspNetCore.Identity;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.Contexts.Identity.Infrastructure.Tokens;
using SuperTutor.Contexts.Identity.Persistence.Entities;

namespace SuperTutor.Contexts.Identity.Infrastructure.Users;

public class UserService : IUserService
{
    private const string InvalidLoginCredentialsErrorMessage = "Invalid login credentials.";

    private readonly UserManager<User> userManager;
    private readonly ITokenService tokenService;

    public UserService(UserManager<User> userManager, ITokenService tokenService)
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
    }

    public async Task<Result<string>> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result.Fail(InvalidLoginCredentialsErrorMessage);
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, password);

        if (!isPasswordValid)
        {
            return Result.Fail(InvalidLoginCredentialsErrorMessage);
        }

        var token = await tokenService.GenerateToken(user);

        return Result.Ok(token);
    }

    public async Task<Result> Register(string email, string username, string plainPassword)
    {
        var user = new User(email, username);
        var identityResult = await userManager.CreateAsync(user, plainPassword);

        if (!identityResult.Succeeded)
        {
            var identityErrorDescriptions = identityResult.Errors.Select(identityError => identityError.Description);

            return new Result().WithErrors(identityErrorDescriptions);
        }

        return Result.Ok();
    }
}
