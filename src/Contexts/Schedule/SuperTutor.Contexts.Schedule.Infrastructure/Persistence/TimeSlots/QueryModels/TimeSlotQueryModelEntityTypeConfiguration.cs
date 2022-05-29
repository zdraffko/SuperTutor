using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.Enumerations;
using SuperTutor.Contexts.Schedule.Domain.TimeSlots.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Schedule.Infrastructure.Persistence.TimeSlots.QueryModels;

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

        builder.Property(timeSlot => timeSlot.Date)
            .HasConversion(
                date => date.ToDateTime(TimeOnly.MinValue),
                dateTime => DateOnly.FromDateTime(dateTime))
            .IsRequired();

        builder.Property(timeSlot => timeSlot.StartTime)
            .HasConversion(
                time => time.ToTimeSpan(),
                timeSpan => TimeOnly.FromTimeSpan(timeSpan))
            .IsRequired();

        builder.Property(timeSlot => timeSlot.Type)
            .HasConversion(
                timeSlotType => timeSlotType.Value,
                timeSlotTypeValue => Enumeration.FromValue<TimeSlotType>(timeSlotTypeValue)!)
            .IsRequired();

        builder.Property(timeSlot => timeSlot.Status)
            .HasConversion(
                timeSlotStatus => timeSlotStatus.Value,
                timeSlotStatusValue => Enumeration.FromValue<TimeSlotStatus>(timeSlotStatusValue)!)
            .IsRequired();
    }
}
