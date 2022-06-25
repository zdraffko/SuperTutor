using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms;
using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;

namespace SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Classrooms.EntityTypeConfigurations;

internal class ClassroomEntityTypeConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        builder.ToTable("Classrooms");

        builder.HasKey(classroom => classroom.Id);

        builder.Property(classroom => classroom.Id)
            .HasConversion(
                classroomId => classroomId.Value,
                classroomIdValue => new ClassroomId(classroomIdValue))
            .IsRequired();

        builder.Property(classroom => classroom.LessonId)
            .HasConversion(
                lessonId => lessonId.Value,
                lessonIdValue => new LessonId(lessonIdValue))
            .IsRequired();

        builder.Property(classroom => classroom.TutorId)
            .HasConversion(
                tutorId => tutorId.Value,
                tutorIdValue => new TutorId(tutorIdValue))
            .IsRequired();

        builder.Property(classroom => classroom.StudentId)
            .HasConversion(
                studentId => studentId!.Value,
                studentIdValue => new StudentId(studentIdValue));

        builder.Property(classroom => classroom.NotebookContent);

        builder.Property(classroom => classroom.WhiteboardContent);

        builder.Property(classroom => classroom.IsActive).IsRequired();

    }
}
