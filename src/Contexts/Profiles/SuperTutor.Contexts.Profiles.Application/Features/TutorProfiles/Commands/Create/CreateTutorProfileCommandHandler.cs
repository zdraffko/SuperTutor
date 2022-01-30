using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
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
        var tutoringGrades = Enumeration.FromValues<Grade>(command.TutoringGrades).ToHashSet();

        var tutorProfile = new TutorProfile(command.TutorId, command.About, tutoringSubject!, tutoringGrades, command.RateForOneHour);

        tutorProfileRepository.Add(tutorProfile);

        return await Task.FromResult(Result.Ok());
    }
}
