using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Users.TutorRegistered;

internal class CreateTutorCommandHandler : ICommandHandler<CreateTutorCommand>
{
    private readonly ITutorRepository tutorRepository;

    public CreateTutorCommandHandler(ITutorRepository tutorRepository) => this.tutorRepository = tutorRepository;

    public Task<Result> Handle(CreateTutorCommand command, CancellationToken cancellationToken)
    {
        var tutor = new Tutor(command.TutorId, command.FirstName, command.LastName);

        tutorRepository.Add(tutor);

        return Task.FromResult(Result.Ok());
    }
}
