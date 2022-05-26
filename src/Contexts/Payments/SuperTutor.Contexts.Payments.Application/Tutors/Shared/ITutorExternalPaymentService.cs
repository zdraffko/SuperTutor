using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;

namespace SuperTutor.Contexts.Payments.Application.Shared;

public interface ITutorExternalPaymentService
{
    Task<Result<(string accountId, string personId)>> CreateAccount(TutorId tutorId, string tutorEmail, CancellationToken cancellationToken);

    Task<Result> UpdatePersonalInformation(string accountId, string personId, PersonalInformation personalInformation, CancellationToken cancellationToken);

    Task<Result> UpdateAddress(string accountId, string personId, Address address, CancellationToken cancellationToken);

    Task<Result> UpdateBankAccount(string accountId, BankAccount bankAccount, CancellationToken cancellationToken);

    Task<Result> UpdateVerificationDocuments(string accountId, string personId, Document identityVerificationDocumentFront, Document identityVerificationDocumentBack, Document addressVerificationDocument, CancellationToken cancellationToken);

    Task<Result<(string fileId, string fileName, string fileUrl)>> UploadIdentityDocument(Stream identityDocument, CancellationToken cancellationToken);
}
