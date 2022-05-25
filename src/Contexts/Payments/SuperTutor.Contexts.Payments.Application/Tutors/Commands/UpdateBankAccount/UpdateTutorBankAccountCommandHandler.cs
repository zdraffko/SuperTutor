using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdateBankAccount;

internal class UpdateTutorBankAccountCommandHandler : ICommandHandler<UpdateTutorBankAccountCommand>
{
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public UpdateTutorBankAccountCommandHandler(IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository) => this.tutorRepository = tutorRepository;

    public async Task<Result> Handle(UpdateTutorBankAccountCommand command, CancellationToken cancellationToken)
    {
        var tutor = await tutorRepository.Load(command.TutorId, cancellationToken);
        if (tutor is null)
        {
            return Result.Fail($"Tutor with Id {command.TutorId} was not found");
        }

        var bankAccount = new BankAccount(command.BankAccountHolderFullName, command.BankAccountHolderType, command.BankAccountIban);

        tutor.UpdateBankAccount(bankAccount);

        await tutorRepository.Update(tutor, cancellationToken);

        return Result.Ok();
    }
}
