using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.AddStudySubjects;

internal class AddStudySubjectsToStudentProfileCommandHandler : ICommandHandler<AddStudySubjectsToStudentProfileCommand>
{
    private readonly IStudentProfileRepository studentProfileRepository;

    public AddStudySubjectsToStudentProfileCommandHandler(IStudentProfileRepository studentProfileRepository)
    {
        this.studentProfileRepository = studentProfileRepository;
    }

    public async Task<Result> Handle(AddStudySubjectsToStudentProfileCommand command, CancellationToken cancellationToken)
    {
        var studentProfile = await studentProfileRepository.GetById(command.StudentProfileId, cancellationToken);
        if (studentProfile is null)
        {
            return Result.Fail("Student profile not found.");
        }

        var newStudySubjects = Enumeration.FromValues<Subject>(command.NewStudySubjects).ToHashSet();

        studentProfile.AddStudySubjects(newStudySubjects);

        return Result.Ok();
    }
}
