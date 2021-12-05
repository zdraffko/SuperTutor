using FluentResults;

namespace SuperTutor.Contexts.Identity.Application.Contracts.Users;

public interface IUserService
{
    Task<Result<string>> Login(string email, string password);

    Task<Result> Register(string email, string username, string plainPassword);
}
