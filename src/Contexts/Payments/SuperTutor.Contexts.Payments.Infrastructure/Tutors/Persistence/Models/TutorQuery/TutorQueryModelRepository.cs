using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Infrastructure.Shared.Persistence;

namespace SuperTutor.Contexts.Payments.Infrastructure.Tutors.Persistence.Models.TutorQuery;

internal class TutorQueryModelRepository : ITutorQueryModelRepository
{
    private readonly PaymentsDbContext paymentsDbContext;

    public TutorQueryModelRepository(PaymentsDbContext paymentsDbContext) => this.paymentsDbContext = paymentsDbContext;

    public async Task Create(TutorId tutorId, CancellationToken cancellationToken)
    {
        var tutorQueryModel = new TutorQueryModel
        {
            Id = tutorId
        };

        paymentsDbContext.Tutors.Add(tutorQueryModel);

        await paymentsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> GetAreTermsOfServiceAccepted(TutorId tutorId, CancellationToken cancellationToken)
        => await paymentsDbContext.Tutors
            .AsNoTracking()
            .Where(tutorQueryModel => tutorQueryModel.Id == tutorId)
            .Select(tutorQueryModel => tutorQueryModel.AreTermsOfServiceAccepted)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<bool> GetAreVerificationDocumentsCollected(TutorId tutorId, CancellationToken cancellationToken)
        => await paymentsDbContext.Tutors
            .AsNoTracking()
            .Where(tutorQueryModel => tutorQueryModel.Id == tutorId)
            .Select(tutorQueryModel => tutorQueryModel.AreVerificationDocumentsCollected)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<bool> GetIsAddressInformationCollected(TutorId tutorId, CancellationToken cancellationToken)
        => await paymentsDbContext.Tutors
            .AsNoTracking()
            .Where(tutorQueryModel => tutorQueryModel.Id == tutorId)
            .Select(tutorQueryModel => tutorQueryModel.IsAddressInformationCollected)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<bool> GetIsBankAccountInformationCollected(TutorId tutorId, CancellationToken cancellationToken)
        => await paymentsDbContext.Tutors
            .AsNoTracking()
            .Where(tutorQueryModel => tutorQueryModel.Id == tutorId)
            .Select(tutorQueryModel => tutorQueryModel.IsBankAccountInformationCollected)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<bool> GetIsPersonalInformationCollected(TutorId tutorId, CancellationToken cancellationToken)
        => await paymentsDbContext.Tutors
            .AsNoTracking()
            .Where(tutorQueryModel => tutorQueryModel.Id == tutorId)
            .Select(tutorQueryModel => tutorQueryModel.IsPersonalInformationCollected)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task SetAddressInformationAsCollected(TutorId tutorId, CancellationToken cancellationToken)
    {
        var updatedTutorQueryModel = new TutorQueryModel
        {
            Id = tutorId,
            IsAddressInformationCollected = true
        };

        paymentsDbContext.Attach(updatedTutorQueryModel);
        paymentsDbContext.Entry(updatedTutorQueryModel).Property(tutorQueryModel => tutorQueryModel.IsAddressInformationCollected).IsModified = true;

        await paymentsDbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task SetBankAccountInformationAsCollected(TutorId tutorId, CancellationToken cancellationToken)
    {
        var updatedTutorQueryModel = new TutorQueryModel
        {
            Id = tutorId,
            IsBankAccountInformationCollected = true
        };

        paymentsDbContext.Attach(updatedTutorQueryModel);
        paymentsDbContext.Entry(updatedTutorQueryModel).Property(tutorQueryModel => tutorQueryModel.IsBankAccountInformationCollected).IsModified = true;

        await paymentsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetPersonalInformationAsCollected(TutorId tutorId, CancellationToken cancellationToken)
    {
        var updatedTutorQueryModel = new TutorQueryModel
        {
            Id = tutorId,
            IsPersonalInformationCollected = true
        };

        paymentsDbContext.Attach(updatedTutorQueryModel);
        paymentsDbContext.Entry(updatedTutorQueryModel).Property(tutorQueryModel => tutorQueryModel.IsPersonalInformationCollected).IsModified = true;

        await paymentsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetTermsOfServiceAsAccepted(TutorId tutorId, CancellationToken cancellationToken)
    {
        var updatedTutorQueryModel = new TutorQueryModel
        {
            Id = tutorId,
            AreTermsOfServiceAccepted = true
        };

        paymentsDbContext.Attach(updatedTutorQueryModel);
        paymentsDbContext.Entry(updatedTutorQueryModel).Property(tutorQueryModel => tutorQueryModel.AreTermsOfServiceAccepted).IsModified = true;

        await paymentsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetVerificationDocumentsAsCollected(TutorId tutorId, CancellationToken cancellationToken)
    {
        var updatedTutorQueryModel = new TutorQueryModel
        {
            Id = tutorId,
            AreVerificationDocumentsCollected = true
        };

        paymentsDbContext.Attach(updatedTutorQueryModel);
        paymentsDbContext.Entry(updatedTutorQueryModel).Property(tutorQueryModel => tutorQueryModel.AreVerificationDocumentsCollected).IsModified = true;

        await paymentsDbContext.SaveChangesAsync(cancellationToken);
    }
}
