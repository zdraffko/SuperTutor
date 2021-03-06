using FluentResults;
using SuperTutor.Contexts.Identity.Domain.Users;

namespace SuperTutor.Contexts.Identity.Application.Contracts.Users;

public interface IUserService
{
    Task<User?> GetById(Guid userId);

    Task<Result<string>> Login(string email, string password);

    Task<Result<Guid>> RegisterTutor(string email, string plainPassword, string firstName, string lastName);

    Task<Result<Guid>> RegisterStudent(string email, string plainPassword, string firstName, string lastName);

    Task<Result<Guid>> RegisterAdmin(string email, string plainPassword, string firstName, string lastName);

    Task<Result<Guid>> Delete(string email);
}
