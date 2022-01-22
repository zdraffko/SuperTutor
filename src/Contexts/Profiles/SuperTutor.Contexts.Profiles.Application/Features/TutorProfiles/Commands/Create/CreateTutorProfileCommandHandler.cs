using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Create;

internal class CreateTutorProfileCommandHandler : ICommandHandler<CreateTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public CreateTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository)
    {
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(CreateTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutoringSubject = Enumeration.FromValue<Subject>(command.TutoringSubject);
        if (tutoringSubject == null)
        {
            return Result.Fail($"A tutoring subject with value '{command.TutoringSubject}' does not exist.");
        }

        var tutoringGrades = Enumeration.FromValues<Grade>(command.TutoringGrades).ToHashSet();
        if (!tutoringGrades.Any())
        {
            return Result.Fail("At least one tutoring grade must be selected.");
        }

        var tutorProfile = new TutorProfile(new TutorId(command.TutorId), command.About, tutoringSubject, tutoringGrades, command.RateForOneHour);

        tutorProfileRepository.Add(tutorProfile);

        return await Task.FromResult(Result.Ok());
    }
}
