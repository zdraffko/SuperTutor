using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.IntegrationEvents.Identity.Users.TutorRegistered;

public class CreateTutorCommandHandler : ICommandHandler<CreateTutorCommand>
{
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public CreateTutorCommandHandler(IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository) => this.tutorRepository = tutorRepository;

    public async Task<Result> Handle(CreateTutorCommand command, CancellationToken cancellationToken)
    {
        var tutor = Tutor.Create(command.UserId, command.Email);

        await tutorRepository.Add(tutor, cancellationToken);

        return Result.Ok();
    }
}
