using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdateAddress;

internal class UpdateTutorAddressCommandHandler : ICommandHandler<UpdateTutorAddressCommand>
{
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public UpdateTutorAddressCommandHandler(IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository) => this.tutorRepository = tutorRepository;

    public async Task<Result> Handle(UpdateTutorAddressCommand command, CancellationToken cancellationToken)
    {
        var tutor = await tutorRepository.Load(command.TutorId, cancellationToken);
        if (tutor is null)
        {
            return Result.Fail($"Tutor with Id {command.TutorId} was not found");
        }

        var address = new Address(command.State, command.City, command.AddressLineOne, command.AddressLineTwo, command.PostalCode);

        tutor.UpdateAddress(address);

        await tutorRepository.Update(tutor, cancellationToken);

        return Result.Ok();
    }
}
