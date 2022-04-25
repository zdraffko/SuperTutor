using FluentResults;
using SuperTutor.Contexts.Catalog.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Catalog.Application.Integration.Profiles.TutorProfiles.Delete;

internal class DeleteTutorProfileCommandHandler : ICommandHandler<DeleteTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public DeleteTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository) => this.tutorProfileRepository = tutorProfileRepository;

    public async Task<Result> Handle(DeleteTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Ok();
        }

        tutorProfileRepository.Remove(tutorProfile);

        return Result.Ok();
    }
}
