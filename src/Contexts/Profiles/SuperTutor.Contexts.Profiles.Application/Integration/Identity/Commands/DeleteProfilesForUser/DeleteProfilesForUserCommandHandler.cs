using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
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
        var allTutorProfilesForUser = await tutorProfileRepository.GetAllForUser(command.UserId, cancellationToken);

        foreach (var tutorProfile in allTutorProfilesForUser)
        {
            tutorProfileRepository.Remove(tutorProfile);
        }

        return Result.Ok();
    }
}
