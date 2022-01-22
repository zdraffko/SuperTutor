using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqrs.Contracts.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.RemoveStudySubject;

internal class RemoveStudySubjectsFromStudentProfileCommandHandler : ICommandHandler<RemoveStudySubjectsFromStudentProfileCommand>
{
    private readonly IStudentProfileRepository studentProfileRepository;

    public RemoveStudySubjectsFromStudentProfileCommandHandler(IStudentProfileRepository studentProfileRepository)
    {
        this.studentProfileRepository = studentProfileRepository;
    }

    public async Task<Result> Handle(RemoveStudySubjectsFromStudentProfileCommand command, CancellationToken cancellationToken)
    {
        var studentProfile = await studentProfileRepository.GetById(new StudentProfileId(command.StudentProfileId), cancellationToken);
        if (studentProfile is null)
        {
            return Result.Fail("Student profile not found.");
        }

        var studySubjectsForRemoval = Enumeration.FromValues<Subject>(command.StudySubjectsForRemoval).ToHashSet();
        if (!studySubjectsForRemoval.Any())
        {
            return Result.Fail("At least one study subject must be selected for removal.");
        }

        studentProfile.RemoveStudySubjects(studySubjectsForRemoval);

        return Result.Ok();
    }
}
