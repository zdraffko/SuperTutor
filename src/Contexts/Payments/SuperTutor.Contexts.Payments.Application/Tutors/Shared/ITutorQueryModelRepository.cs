using SuperTutor.Contexts.Payments.Domain.Tutors;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Shared;

public interface ITutorQueryModelRepository
{
    Task Create(TutorId tutorId, CancellationToken cancellationToken);

    Task<bool> GetIsPersonalInformationCollected(TutorId tutorId, CancellationToken cancellationToken);

    Task<bool> GetIsAddressInformationCollected(TutorId tutorId, CancellationToken cancellationToken);

    Task<bool> GetIsBankAccountInformationCollected(TutorId tutorId, CancellationToken cancellationToken);

    Task<bool> GetAreVerificationDocumentsCollected(TutorId tutorId, CancellationToken cancellationToken);

    Task<bool> GetAreTermsOfServiceAccepted(TutorId tutorId, CancellationToken cancellationToken);

    Task SetPersonalInformationAsCollected(TutorId tutorId, CancellationToken cancellationToken);

    Task SetAddressInformationAsCollected(TutorId tutorId, CancellationToken cancellationToken);

    Task SetBankAccountInformationAsCollected(TutorId tutorId, CancellationToken cancellationToken);

    Task SetVerificationDocumentsAsCollected(TutorId tutorId, CancellationToken cancellationToken);

    Task SetTermsOfServiceAsAccepted(TutorId tutorId, CancellationToken cancellationToken);
}
