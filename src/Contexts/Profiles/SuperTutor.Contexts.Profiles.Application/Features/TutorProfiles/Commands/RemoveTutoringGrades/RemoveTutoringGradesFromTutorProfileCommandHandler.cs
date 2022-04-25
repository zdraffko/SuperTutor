using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.IntegrationEvents.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.IntegrationEvents;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.RemoveTutoringGrades;

internal class RemoveTutoringGradesFromTutorProfileCommandHandler : ICommandHandler<RemoveTutoringGradesFromTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;
    private readonly IIntegrationEventsService integrationEventsService;

    public RemoveTutoringGradesFromTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository, IIntegrationEventsService integrationEventsService)
    {
        this.tutorProfileRepository = tutorProfileRepository;
        this.integrationEventsService = integrationEventsService;
    }

    public async Task<Result> Handle(RemoveTutoringGradesFromTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        var tutoringGradesForRemoval = Enumeration.FromValues<Grade>(command.TutoringGradesForRemoval).ToHashSet();

        tutorProfile.RemoveTutoringGrades(tutoringGradesForRemoval);

        integrationEventsService.Raise(new TutorProfileTutoringGradesRemovedIntegrationEvent(
            tutorProfile.Id.Value,
            tutorProfile.TutoringGrades.Select(tutoringGrade => new TutorProfileTutoringGradesRemovedIntegrationEvent.Grade(tutoringGrade.Value, tutoringGrade.Name))));

        return Result.Ok();
    }
}
