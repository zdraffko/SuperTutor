﻿using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Integration.Identity.Commands.DeleteProfilesForUser;

internal class DeleteProfilesForUserCommandHandler : ICommandHandler<DeleteProfilesForUserCommand>
{
    private readonly ITutorProfileRepository profileRepository;

    public DeleteProfilesForUserCommandHandler(ITutorProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(DeleteProfilesForUserCommand command, CancellationToken cancellationToken)
    {
        var allUserProfiles = await profileRepository.GetAllForUser(command.UserId, cancellationToken);

        foreach (var profile in allUserProfiles)
        {
            profileRepository.Remove(profile);
        }

        return Result.Ok();
    }
}
