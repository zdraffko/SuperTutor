using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Payments.Application.Tutors.Shared;
using SuperTutor.Contexts.Payments.Domain.Tutors;

namespace SuperTutor.Contexts.Payments.Infrastructure.Tutors.Persistence.Models.TutorQuery;

internal class TutorQueryModelRepository : ITutorQueryModelRepository
{
    private readonly ITutorQueryModelDbContext tutorQueryModelDbContext;

    public TutorQueryModelRepository(ITutorQueryModelDbContext tutorQueryModelDbContext) => this.tutorQueryModelDbContext = tutorQueryModelDbContext;

    public async Task<bool> GetAreTermsOfServiceAccepted(TutorId tutorId, CancellationToken cancellationToken)
        => await tutorQueryModelDbContext.Tutors
            .AsNoTracking()
            .Where(tutorQueryModel => tutorQueryModel.Id == tutorId)
            .Select(tutorQueryModel => tutorQueryModel.AreTermsOfServiceAccepted)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<bool> GetAreVerificationDocumentsCollected(TutorId tutorId, CancellationToken cancellationToken)
        => await tutorQueryModelDbContext.Tutors
            .AsNoTracking()
            .Where(tutorQueryModel => tutorQueryModel.Id == tutorId)
            .Select(tutorQueryModel => tutorQueryModel.AreVerificationDocumentsCollected)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<bool> GetIsAddressInformationCollected(TutorId tutorId, CancellationToken cancellationToken)
        => await tutorQueryModelDbContext.Tutors
            .AsNoTracking()
            .Where(tutorQueryModel => tutorQueryModel.Id == tutorId)
            .Select(tutorQueryModel => tutorQueryModel.IsAddressInformationCollected)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<bool> GetIsBankAccountInformationCollected(TutorId tutorId, CancellationToken cancellationToken)
        => await tutorQueryModelDbContext.Tutors
            .AsNoTracking()
            .Where(tutorQueryModel => tutorQueryModel.Id == tutorId)
            .Select(tutorQueryModel => tutorQueryModel.IsBankAccountInformationCollected)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<bool> GetIsPersonalInformationCollected(TutorId tutorId, CancellationToken cancellationToken)
        => await tutorQueryModelDbContext.Tutors
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

        tutorQueryModelDbContext.Tutors.Update(updatedTutorQueryModel);

        await tutorQueryModelDbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task SetBankAccountInformationAsCollected(TutorId tutorId, CancellationToken cancellationToken)
    {
        var updatedTutorQueryModel = new TutorQueryModel
        {
            Id = tutorId,
            IsBankAccountInformationCollected = true
        };

        tutorQueryModelDbContext.Tutors.Update(updatedTutorQueryModel);

        await tutorQueryModelDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetPersonalInformationAsCollected(TutorId tutorId, CancellationToken cancellationToken)
    {
        var updatedTutorQueryModel = new TutorQueryModel
        {
            Id = tutorId,
            IsPersonalInformationCollected = true
        };

        tutorQueryModelDbContext.Tutors.Update(updatedTutorQueryModel);

        await tutorQueryModelDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetTermsOfServiceAsAccepted(TutorId tutorId, CancellationToken cancellationToken)
    {
        var updatedTutorQueryModel = new TutorQueryModel
        {
            Id = tutorId,
            AreTermsOfServiceAccepted = true
        };

        tutorQueryModelDbContext.Tutors.Update(updatedTutorQueryModel);

        await tutorQueryModelDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetVerificationDocumentsAsCollected(TutorId tutorId, CancellationToken cancellationToken)
    {
        var updatedTutorQueryModel = new TutorQueryModel
        {
            Id = tutorId,
            AreVerificationDocumentsCollected = true
        };

        tutorQueryModelDbContext.Tutors.Update(updatedTutorQueryModel);

        await tutorQueryModelDbContext.SaveChangesAsync(cancellationToken);
    }
}
