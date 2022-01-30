using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.UpdateStudyGrade;

internal class UpdateStudyGradeForStudentProfileCommandHandler : ICommandHandler<UpdateStudyGradeForStudentProfileCommand>
{
    private readonly IStudentProfileRepository studentProfileRepository;

    public UpdateStudyGradeForStudentProfileCommandHandler(IStudentProfileRepository studentProfileRepository)
    {
        this.studentProfileRepository = studentProfileRepository;
    }

    public async Task<Result> Handle(UpdateStudyGradeForStudentProfileCommand command, CancellationToken cancellationToken)
    {
        var studentProfile = await studentProfileRepository.GetById(command.StudentProfileId, cancellationToken);
        if (studentProfile is null)
        {
            return Result.Fail("Student profile not found.");
        }

        var newStudyGrade = Enumeration.FromValue<Grade>(command.NewStudyGrade);
        if (newStudyGrade is null)
        {
            return Result.Fail($"A study grade with value '{command.NewStudyGrade}' does not exist.");
        }

        studentProfile.UpdateStudyGrade(newStudyGrade);

        return Result.Ok();
    }
}
