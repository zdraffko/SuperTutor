using FluentResults;

namespace SuperTutor.Contexts.Identity.Application.Contracts.Users;

public interface IUserService
{
    Task<Result> Register(string email, string username, string plainPassword);
}
