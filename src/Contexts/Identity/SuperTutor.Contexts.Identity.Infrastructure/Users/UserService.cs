using FluentResults;
using Microsoft.AspNetCore.Identity;
using SuperTutor.Contexts.Identity.Application.Contracts.Users;
using SuperTutor.Contexts.Identity.Persistence.Entities;

namespace SuperTutor.Contexts.Identity.Infrastructure.Users;

public class UserService : IUserService
{
    private readonly UserManager<User> userManager;

    public UserService(UserManager<User> userManager)
    {
        this.userManager = userManager;
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
