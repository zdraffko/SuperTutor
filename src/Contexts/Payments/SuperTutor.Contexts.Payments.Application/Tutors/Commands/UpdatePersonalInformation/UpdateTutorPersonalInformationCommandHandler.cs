using FluentResults;
using SuperTutor.Contexts.Payments.Domain.Tutors;
using SuperTutor.Contexts.Payments.Domain.Tutors.Models.ValueObjects;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Repositories.Contracts;

namespace SuperTutor.Contexts.Payments.Application.Tutors.Commands.UpdatePersonalInformation;

internal class UpdateTutorPersonalInformationCommandHandler : ICommandHandler<UpdateTutorPersonalInformationCommand>
{
    private readonly IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository;

    public UpdateTutorPersonalInformationCommandHandler(IAggregateRootEventsRepository<Tutor, TutorId, Guid> tutorRepository) => this.tutorRepository = tutorRepository;

    public async Task<Result> Handle(UpdateTutorPersonalInformationCommand command, CancellationToken cancellationToken)
    {
        var tutor = await tutorRepository.Load(command.TutorId, cancellationToken);
        if (tutor is null)
        {
            return Result.Fail($"Tutor with Id {command.TutorId} was not found");
        }

        var personalInformation = new PersonalInformation(command.FirstName, command.LastName, command.DateOfBirth.Day, command.DateOfBirth.Month, command.DateOfBirth.Year);

        tutor.UpdatePersonalInformation(personalInformation);

        await tutorRepository.Update(tutor, cancellationToken);

        return Result.Ok();
    }
}
