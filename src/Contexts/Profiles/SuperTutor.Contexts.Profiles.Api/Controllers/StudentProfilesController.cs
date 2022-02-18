using Microsoft.AspNetCore.Mvc;
using SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.AddStudySubjects;
using SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.Create;
using SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.RemoveStudySubjects;
using SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.UpdateStudyGrade;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;

namespace SuperTutor.Contexts.Profiles.Api.Controllers;

public class StudentProfilesController : ApiController
{
    private readonly ICommandHandler<CreateStudentProfileCommand> createStudentProfileCommandHandler;
    private readonly ICommandHandler<AddStudySubjectsToStudentProfileCommand> addStudySubjectsToStudentProfileCommandHandler;
    private readonly ICommandHandler<RemoveStudySubjectsFromStudentProfileCommand> removeStudySubjectsFromStudentProfileCommandHandler;
    private readonly ICommandHandler<UpdateStudyGradeForStudentProfileCommand> updateStudyGradeForStudentProfileCommandHandler;

    public StudentProfilesController(
        ICommandHandler<CreateStudentProfileCommand> createStudentProfileCommandHandler,
        ICommandHandler<AddStudySubjectsToStudentProfileCommand> addStudySubjectsToStudentProfileCommandHandler,
        ICommandHandler<RemoveStudySubjectsFromStudentProfileCommand> removeStudySubjectsFromStudentProfileCommandHandler,
        ICommandHandler<UpdateStudyGradeForStudentProfileCommand> updateStudyGradeForStudentProfileCommandHandler)
    {
        this.createStudentProfileCommandHandler = createStudentProfileCommandHandler;
        this.addStudySubjectsToStudentProfileCommandHandler = addStudySubjectsToStudentProfileCommandHandler;
        this.removeStudySubjectsFromStudentProfileCommandHandler = removeStudySubjectsFromStudentProfileCommandHandler;
        this.updateStudyGradeForStudentProfileCommandHandler = updateStudyGradeForStudentProfileCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateStudentProfileCommand command, CancellationToken cancellationToken)
        => await Handle(createStudentProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> AddStudySubjects(AddStudySubjectsToStudentProfileCommand command, CancellationToken cancellationToken)
        => await Handle(addStudySubjectsToStudentProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> RemoveStudySubjects(RemoveStudySubjectsFromStudentProfileCommand command, CancellationToken cancellationToken)
        => await Handle(removeStudySubjectsFromStudentProfileCommandHandler, command, cancellationToken);

    [HttpPost]
    public async Task<ActionResult> UpdateStudyGrade(UpdateStudyGradeForStudentProfileCommand command, CancellationToken cancellationToken)
        => await Handle(updateStudyGradeForStudentProfileCommandHandler, command, cancellationToken);
}
