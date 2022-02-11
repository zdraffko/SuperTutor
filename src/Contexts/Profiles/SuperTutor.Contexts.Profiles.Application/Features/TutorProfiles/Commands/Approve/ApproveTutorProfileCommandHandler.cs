﻿using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.Approve;

internal class ApproveTutorProfileCommandHandler : ICommandHandler<ApproveTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public ApproveTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository)
    {
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(ApproveTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found");
        }

        tutorProfile.Approve(command.AdminId);

        return Result.Ok();
    }
}
