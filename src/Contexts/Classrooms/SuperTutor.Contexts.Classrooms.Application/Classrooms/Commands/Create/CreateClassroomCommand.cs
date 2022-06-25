using SuperTutor.Contexts.Classrooms.Domain.Classrooms.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Classrooms.Application.Classrooms.Commands.Create;

public class CreateClassroomCommand : Command
{
    public CreateClassroomCommand(LessonId lessonId, TutorId tutorId, StudentId studentId)
    {
        LessonId = lessonId;
        TutorId = tutorId;
        StudentId = studentId;
    }

    public LessonId LessonId { get; }

    public TutorId TutorId { get; }

    public StudentId StudentId { get; }
}
