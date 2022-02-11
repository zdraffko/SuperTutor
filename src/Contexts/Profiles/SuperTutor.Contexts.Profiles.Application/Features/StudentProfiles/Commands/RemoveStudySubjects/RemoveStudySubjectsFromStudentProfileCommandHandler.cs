using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
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
        var studentProfile = await studentProfileRepository.GetById(command.StudentProfileId, cancellationToken);
        if (studentProfile is null)
        {
            return Result.Fail("Student profile not found");
        }

        var studySubjectsForRemoval = Enumeration.FromValues<Subject>(command.StudySubjectsForRemoval).ToHashSet();

        studentProfile.RemoveStudySubjects(studySubjectsForRemoval);

        return Result.Ok();
    }
}
