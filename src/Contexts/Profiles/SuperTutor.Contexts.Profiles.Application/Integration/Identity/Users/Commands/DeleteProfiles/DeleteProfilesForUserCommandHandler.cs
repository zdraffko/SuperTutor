using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.Contexts.Profiles.IntegrationEvents.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;

namespace SuperTutor.Contexts.Profiles.Application.Integration.Identity.Users.Commands.DeleteProfiles;

internal class DeleteProfilesForUserCommandHandler : ICommandHandler<DeleteProfilesForUserCommand>
{
    private readonly IStudentProfileRepository studentProfileRepository;
    private readonly ITutorProfileRepository tutorProfileRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public DeleteProfilesForUserCommandHandler(IStudentProfileRepository studentProfileRepository, ITutorProfileRepository tutorProfileRepository, IIntegrationEventsService integrationEventsService)
    {
        this.studentProfileRepository = studentProfileRepository;
        this.tutorProfileRepository = tutorProfileRepository;
        this.integrationEventsService = integrationEventsService;
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

        integrationEventsService.Raise(new StudentProfileDeletedIntegrationEvent(studentId.Value));

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
