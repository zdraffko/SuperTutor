using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Payments.Application.Shared;

public interface ITutorExternalPaymentService
{
    Task<Result<string>> CreateAccount(UserId userId, string email, CancellationToken cancellationToken);
}
