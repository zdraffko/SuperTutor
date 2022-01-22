using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Integration.Identity.Commands.DeleteProfilesForUser;

internal class DeleteProfilesForUserCommandHandler : ICommandHandler<DeleteProfilesForUserCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public DeleteProfilesForUserCommandHandler(ITutorProfileRepository tutorProfileRepository)
    {
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(DeleteProfilesForUserCommand command, CancellationToken cancellationToken)
    {
        var allTutorProfilesForTutor = await tutorProfileRepository.GetAllForTutor(new TutorId(command.UserId), cancellationToken);

        foreach (var tutorProfile in allTutorProfilesForTutor)
        {
            tutorProfileRepository.Remove(tutorProfile);
        }

        return Result.Ok();
    }
}
