using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Payments.Domain.Tutors;

namespace SuperTutor.Contexts.Payments.Infrastructure.Tutors.Persistence.Models.TutorQuery;

internal class TutorQueryModelEntityTypeConfiguration : IEntityTypeConfiguration<TutorQueryModel>
{
    public void Configure(EntityTypeBuilder<TutorQueryModel> builder)
    {
        builder.ToTable("Tutors");

        builder.HasKey(tutorQueryModel => tutorQueryModel.Id);

        builder.Property(tutorQueryModel => tutorQueryModel.Id)
            .HasConversion(
                tutorId => tutorId.Value,
                tutorIdValue => new TutorId(tutorIdValue))
            .IsRequired();

        builder.Property(tutorQueryModel => tutorQueryModel.IsPersonalInformationCollected).IsRequired();

        builder.Property(tutorQueryModel => tutorQueryModel.IsAddressInformationCollected).IsRequired();

        builder.Property(tutorQueryModel => tutorQueryModel.IsBankAccountInformationCollected).IsRequired();

        builder.Property(tutorQueryModel => tutorQueryModel.AreVerificationDocumentsCollected).IsRequired();

        builder.Property(tutorQueryModel => tutorQueryModel.AreTermsOfServiceAccepted).IsRequired();
    }
}
