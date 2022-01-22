using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Integration.Identity.Commands.DeleteProfilesForUser;

internal class DeleteProfilesForUserCommandHandler : ICommandHandler<DeleteProfilesForUserCommand>
{
    private readonly IStudentProfileRepository studentProfileRepository;
    private readonly ITutorProfileRepository tutorProfileRepository;

    public DeleteProfilesForUserCommandHandler(IStudentProfileRepository studentProfileRepository, ITutorProfileRepository tutorProfileRepository)
    {
        this.studentProfileRepository = studentProfileRepository;
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(DeleteProfilesForUserCommand command, CancellationToken cancellationToken)
    {
        var isTheUserAStudent = await TryToDeleteStudentProfile(new StudentId(command.UserId), cancellationToken);
        if (isTheUserAStudent)
        {
            return Result.Ok();
        }

        await TryToDeleteTutorProfiles(new TutorId(command.UserId), cancellationToken);

        return Result.Ok();
    }

    private async Task<bool> TryToDeleteStudentProfile(StudentId studentId, CancellationToken cancellationToken)
    {
        var studentProfile = await studentProfileRepository.GetByStudentId(studentId, cancellationToken);
        if (studentProfile is null)
        {
            return false;
        }

        studentProfileRepository.Remove(studentProfile);

        return true;
    }

    private async Task<bool> TryToDeleteTutorProfiles(TutorId tutorId, CancellationToken cancellationToken)
    {
        var tutorProfiles = await tutorProfileRepository.GetAllForTutor(tutorId, cancellationToken);
        if (!tutorProfiles.Any())
        {
            return false;
        }

        foreach (var tutorProfile in tutorProfiles)
        {
            tutorProfileRepository.Remove(tutorProfile);
        }

        return true;
    }
}
