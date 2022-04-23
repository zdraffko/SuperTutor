using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.Create;

internal class CreateStudentProfileCommandHandler : ICommandHandler<CreateStudentProfileCommand>
{
    private readonly IStudentProfileRepository studentProfileRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public CreateStudentProfileCommandHandler(IStudentProfileRepository studentProfileRepository, IIntegrationEventsService integrationEventsService)
    {
        this.studentProfileRepository = studentProfileRepository;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(CreateStudentProfileCommand command, CancellationToken cancellationToken)
    {
        var studySubjects = Enumeration.FromValues<Subject>(command.StudySubjects).ToHashSet();
        var studyGrade = Enumeration.FromValue<Grade>(command.StudyGrade);

        var studentProfile = new StudentProfile(command.StudentId, studySubjects, studyGrade!);

        studentProfileRepository.Add(studentProfile);

        integrationEventsService.Raise(new StudentProfileCreatedIntegrationEvent(command.StudentId.Value));

        return await Task.FromResult(Result.Ok());
    }
}
