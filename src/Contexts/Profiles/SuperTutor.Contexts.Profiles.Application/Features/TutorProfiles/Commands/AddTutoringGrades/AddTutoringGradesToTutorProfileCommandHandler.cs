using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.TutorProfiles.Commands.AddTutoringGrades;

internal class AddTutoringGradesToTutorProfileCommandHandler : ICommandHandler<AddTutoringGradesToTutorProfileCommand>
{
    private readonly ITutorProfileRepository tutorProfileRepository;

    public AddTutoringGradesToTutorProfileCommandHandler(ITutorProfileRepository tutorProfileRepository)
    {
        this.tutorProfileRepository = tutorProfileRepository;
    }

    public async Task<Result> Handle(AddTutoringGradesToTutorProfileCommand command, CancellationToken cancellationToken)
    {
        var tutorProfile = await tutorProfileRepository.GetById(command.TutorProfileId, cancellationToken);
        if (tutorProfile is null)
        {
            return Result.Fail("Tutor profile not found.");
        }

        var newTutoringGrades = Enumeration.FromValues<Grade>(command.NewTutoringGrades).ToHashSet();
        if (!newTutoringGrades.Any())
        {
            return Result.Fail("At least one tutoring grade must be selected to be added.");
        }

        tutorProfile.AddTutoringGrades(newTutoringGrades);

        return Result.Ok();
    }
}
