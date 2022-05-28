using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.Create;

internal class CreateTutorProfileCommandHandler : ICommandHandler<CreateTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public CreateTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository) => this.tutorProfileRepository = tutorProfileRepository;

    public Task<Result> Handle(CreateTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = new TutorProfile(
            command.TutorId,
            command.TutorProfileId,
            command.About,
            command.TutoringSubject,
            command.TutoringGrades.ToList(),
            command.RateForOneHour,
            command.IsActive);

        tutorProfileRepository.Add(tutorProfile);

        return Task.FromResult(Result.Ok());
    }
}
