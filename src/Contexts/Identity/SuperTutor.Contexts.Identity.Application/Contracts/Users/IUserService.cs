using FluentResults;

namespace SuperTutor.Contexts.Identity.Application.Contracts.Users;

public interface IUserService
{
    Task<Result<string>> Login(string email, string password);

    Task<Result> RegisterTutor(string email, string plainPassword);

    Task<Result> RegisterStudent(string email, string plainPassword);

    Task<Result<Guid>> Delete(string email);
}
