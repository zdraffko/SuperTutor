using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.AddTutoringGrades;

internal class AddTutoringGradesToTutorProfileCommandHandler : ICommandHandler<AddTutoringGradesToTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public AddTutoringGradesToTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository, IIntegrationEventsService integrationEventsService)
    {
        this.tutorProfileRepository = tutorProfileRepository;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(AddTutoringGradesToTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        var newTutoringGrades = Enumeration.FromValues<Grade>(command.NewTutoringGrades).ToHashSet();

        tutorProfile.AddTutoringGrades(newTutoringGrades);

        integrationEventsService.Raise(new TutorProfileTutoringGradesAddedIntegrationEvent(
            tutorProfile.Id.Value,
            tutorProfile.TutoringGrades.Select(tutoringGrade => new TutorProfileTutoringGradesAddedIntegrationEvent.Grade(tutoringGrade.Value, tutoringGrade.Name))));

        return Result.Ok();
    }
}
