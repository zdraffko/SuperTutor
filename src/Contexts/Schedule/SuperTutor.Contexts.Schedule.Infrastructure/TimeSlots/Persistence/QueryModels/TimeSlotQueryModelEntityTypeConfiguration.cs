using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Schedule.Application.TimeSlots.Shared;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Schedule.Infrastructure.TimeSlots.Persistence.QueryModels;

internal class TimeSlotQueryModelEntityTypeConfiguration : IEntityTypeConfiguration<TimeSlotQueryModel>
{
    public void Configure(EntityTypeBuilder<TimeSlotQueryModel> builder)
    {
        builder.ToTable("TimeSlots");

        builder.HasKey(timeSlot => timeSlot.Id);

        builder.Property(timeSlot => timeSlot.Id)
            .HasConversion(
                timeSlotId => timeSlotId.Value,
                timeSlotIdValue => new TimeSlotId(timeSlotIdValue))
            .IsRequired();

        builder.Property(timeSlot => timeSlot.TutorId)
            .HasConversion(
                tutorId => tutorId.Value,
                tutorIdValue => new TutorId(tutorIdValue))
            .IsRequired();

        builder.Property(timeSlot => timeSlot.Date).IsRequired();

        builder.Property(timeSlot => timeSlot.StartTime).IsRequired();

        builder.Property(timeSlot => timeSlot.Type).IsRequired();

        builder.Property(timeSlot => timeSlot.Status).IsRequired();
    }
}
