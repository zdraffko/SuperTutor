﻿using FluentResults;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Cqs.Commands;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Enumerations;

namespace SuperTutor.Contexts.Profiles.Application.Features.StudentProfiles.Commands.Create;

internal class CreateStudentProfileCommandHandler : ICommandHandler<CreateStudentProfileCommand>
{
    private readonly IStudentProfileRepository studentProfileRepository;

    public CreateStudentProfileCommandHandler(IStudentProfileRepository studentProfileRepository) => this.studentProfileRepository = studentProfileRepository;

    public async Task<Result> Handle(CreateStudentProfileCommand command, CancellationToken cancellationToken)
    {
        var studySubjects = Enumeration.FromValues<Subject>(command.StudySubjects).ToHashSet();
        var studyGrade = Enumeration.FromValue<Grade>(command.StudyGrade);

        var studentProfile = new StudentProfile(command.StudentId, studySubjects, studyGrade!);

        studentProfileRepository.Add(studentProfile);

        return await Task.FromResult(Result.Ok());
    }
}
