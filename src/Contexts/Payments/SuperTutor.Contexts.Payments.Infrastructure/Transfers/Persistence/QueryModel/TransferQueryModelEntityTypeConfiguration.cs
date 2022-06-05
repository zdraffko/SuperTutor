using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Payments.Application.Transfers.Queries;
using SuperTutor.Contexts.Payments.Domain.Charges;
using SuperTutor.Contexts.Payments.Domain.Shared.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Payments.Domain.Transfers;
using SuperTutor.Contexts.Payments.Domain.Tutors;

namespace SuperTutor.Contexts.Payments.Infrastructure.Transfers.Persistence.QueryModel;

internal class TransferQueryModelEntityTypeConfiguration : IEntityTypeConfiguration<TransferQueryModel>
{
    public void Configure(EntityTypeBuilder<TransferQueryModel> builder)
    {
        builder.ToTable("Transfers");

        builder.HasKey(transfer => transfer.Id);

        builder.Property(transfer => transfer.Id)
            .HasConversion(
                transferId => transferId.Value,
                transferIdValue => new TransferId(transferIdValue))
            .IsRequired();

        builder.Property(transfer => transfer.ChargeId)
            .HasConversion(
                chargeId => chargeId.Value,
                chargeIdValue => new ChargeId(chargeIdValue))
            .IsRequired();

        builder.Property(transfer => transfer.LessonId)
            .HasConversion(
                lessonId => lessonId.Value,
                lessonIdValue => new LessonId(lessonIdValue))
            .IsRequired();

        builder.Property(transfer => transfer.StudentId)
            .HasConversion(
                studentId => studentId.Value,
                studentIdValue => new StudentId(studentIdValue))
            .IsRequired();

        builder.Property(transfer => transfer.TutorId)
            .HasConversion(
                tutorId => tutorId.Value,
                tutorIdValue => new TutorId(tutorIdValue))
            .IsRequired();

        builder.Property(timeSlot => timeSlot.Amount).IsRequired();

        builder.Property(timeSlot => timeSlot.Currency).IsRequired();
    }
}
