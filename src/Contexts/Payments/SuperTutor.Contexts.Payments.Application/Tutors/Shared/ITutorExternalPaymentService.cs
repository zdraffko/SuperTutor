using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Payments.Application.Shared;

public interface ITutorExternalPaymentService
{
    Task<Result<(string accountId, string personId)>> CreateAccount(UserId userId, string email, CancellationToken cancellationToken);
}
