using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Schedule.Application.Lessons.Queries;
using SuperTutor.Contexts.Schedule.Domain.Common.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Schedule.Domain.Lessons;
using SuperTutor.Contexts.Schedule.Domain.Lessons.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Schedule.Infrastructure.Lessons.Persistence.QueryModels;

internal class LessonQueryModelEntityTypeConfiguration : IEntityTypeConfiguration<LessonQueryModel>
{
    public void Configure(EntityTypeBuilder<LessonQueryModel> builder)
    {
        builder.ToTable("Lessons");

        builder.HasKey(lesson => lesson.Id);

        builder.Property(lesson => lesson.Id)
            .HasConversion(
                lessonId => lessonId.Value,
                lessonIdValue => new LessonId(lessonIdValue))
            .IsRequired();

        builder.Property(lesson => lesson.TutorId)
            .HasConversion(
                tutorId => tutorId.Value,
                tutorIdValue => new TutorId(tutorIdValue))
            .IsRequired();

        builder.Property(lesson => lesson.StudentId)
            .HasConversion(
                studentId => studentId.Value,
                studentIdValue => new StudentId(studentIdValue))
            .IsRequired();

        builder.Property(timeSlot => timeSlot.Date).IsRequired();

        builder.Property(timeSlot => timeSlot.StartTime).IsRequired();

        builder.Property(timeSlot => timeSlot.Duration).IsRequired();

        builder.Property(timeSlot => timeSlot.Subject).IsRequired();

        builder.Property(timeSlot => timeSlot.Grade).IsRequired();

        builder.Property(timeSlot => timeSlot.Type).IsRequired();

        builder.Property(timeSlot => timeSlot.Status).IsRequired();

        builder.Property(timeSlot => timeSlot.PaymentStatus).IsRequired();
    }
}
