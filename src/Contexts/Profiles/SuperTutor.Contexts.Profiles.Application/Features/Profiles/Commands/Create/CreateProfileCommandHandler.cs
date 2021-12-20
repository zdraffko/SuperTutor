using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Profiles;
using SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Enumerations;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.Profiles.Commands.Create;

internal class CreateProfileCommandHandler : ICommandHandler<CreateProfileCommand>
{
    private readonly IProfileRepository profileRepository;

    public CreateProfileCommandHandler(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<Result> Handle(CreateProfileCommand command, CancellationToken cancellationToken)
    {
        var tutoringSubject = Enumeration.FromValue<TutoringSubject>(command.TutoringSubject);
        if (tutoringSubject == null)
        {
            return Result.Fail($"A tutoring subject with value '{command.TutoringSubject}' does not exist.");
        }

        var tutoringGrades = Enumeration.FromValues<TutoringGrade>(command.TutoringGrades);
        if (!tutoringGrades.Any())
        {
            return Result.Fail("At least one tutoring grade must be selected.");
        }

        var profile = new Profile(command.About, tutoringSubject, tutoringGrades, command.RateForOneHour);

        profileRepository.Add(profile);

        return await Task.FromResult(Result.Ok());
    }
}
