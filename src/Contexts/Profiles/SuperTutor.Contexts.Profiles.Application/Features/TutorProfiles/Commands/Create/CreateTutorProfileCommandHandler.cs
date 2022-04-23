using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Create;

internal class CreateTutorProfileCommandHandler : ICommandHandler<CreateTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public CreateTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository, IIntegrationEventsService integrationEventsService)
    {
        this.tutorProfileRepository = tutorProfileRepository;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(CreateTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutoringSubject = Enumeration.FromValue<Subject>(command.TutoringSubject);
        var tutoringGrades = Enumeration.FromValues<Grade>(command.TutoringGrades).ToHashSet();

        var tutorProfile = new TutorProfile(command.TutorId, command.About, tutoringSubject!, tutoringGrades, command.RateForOneHour);

        tutorProfileRepository.Add(tutorProfile);

        var tutorProfileCreatedIntegrationEvent = new TutorProfileCreatedIntegrationEvent(
            tutorProfile.Id.Value,
            tutorProfile.About,
            tutorProfile.TutoringSubject.Value,
            tutorProfile.TutoringGrades.Select(tutoringGrade => tutoringGrade.Value),
            tutorProfile.RateForOneHour,
            tutorProfile.Status == TutorProfileStatus.Active);

        integrationEventsService.Raise(tutorProfileCreatedIntegrationEvent);

        return await Task.FromResult(Result.Ok());
    }
}
