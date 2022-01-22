﻿using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Activate;

internal class ActivateTutorProfileCommandHandler : ICommandHandler<ActivateTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public ActivateTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository)
    {
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(ActivateTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(new TutorProfileId(command.TutorProfileId), cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found.");
        }

        tutorProfile.Activate();

        return Result.Ok();
    }
}
