using FluentResults;
using SuperTutor.Contexts.Identity.Domain.Users;

namespace SuperTutor.Contexts.Identity.Application.Contracts.Users;

public interface IUserService
{
    Task<User?> GetById(Guid userId);

    Task<Result<string>> Login(string email, string password);

    Task<Result<Guid>> RegisterTutor(string email, string plainPassword);

    Task<Result<Guid>> RegisterStudent(string email, string plainPassword);

    Task<Result<Guid>> Delete(string email);
}
