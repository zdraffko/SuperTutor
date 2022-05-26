using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.AcceptTermsOfService;

internal class AcceptTutorTermsOfServiceCommandHandler : ICommandHandler<AcceptTutorTermsOfServiceCommand>
{
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public AcceptTutorTermsOfServiceCommandHandler(IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository) => this.tutorRepository = tutorRepository;

    public async Task<Result> Handle(AcceptTutorTermsOfServiceCommand command, CancellationToken cancellationToken)
    {
        var tutor = await tutorRepository.Load(command.TutorId, cancellationToken);
        if (tutor is null)
        {
            return Result.Fail($"Tutor with Id {command.TutorId} was not found");
        }

        tutor.AcceptTermsOfService(command.IpOfAcceptance);

        await tutorRepository.Update(tutor, cancellationToken);

        return Result.Ok();
    }
}
