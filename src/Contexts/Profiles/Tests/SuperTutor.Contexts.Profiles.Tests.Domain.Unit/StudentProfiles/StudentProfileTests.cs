using FluentAssertions;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Invariants;
using SuperTutor.Contexts.Profiles.Domain.StudentProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Tests.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SuperTutor.Contexts.Profiles.Tests.Unit.Domain.StudentProfiles;

public class StudentProfileTests
{
    private readonly StudentId DefaultStudentId;
    private readonly HashSet<Subject> DefaultStudySubjects;
    private readonly Grade DefaultStudyGrade;

    public StudentProfileTests()
    {
        DefaultStudentId = new StudentId(Guid.Parse("3E514647-E8B9-4EAB-7447-08D9DDDCD723"));
        DefaultStudySubjects = new HashSet<Subject> { Subject.Math, Subject.Bulgarian };
        DefaultStudyGrade = Grade.Tenth;
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_WhenAllArgumentsAreProvided_ShouldCreateAValidInstance()
    {
        // Arrange

        // Act
        var studentProfile = new StudentProfile(DefaultStudentId, DefaultStudySubjects, DefaultStudyGrade);

        // Assert
        studentProfile.StudentId.Should().BeEquivalentTo(DefaultStudentId);
        studentProfile.StudySubjects.Should().BeEquivalentTo(DefaultStudySubjects);
        studentProfile.StudyGrade.Should().BeEquivalentTo(DefaultStudyGrade);
    }

    [Fact]
    public void Constructor_WhenNoStudySubjectsAreProvided_ShouldBreakStudentProfileMustHaveAtLeastOneStudySubjectInvariant()
    {
        // Arrange
        var studentId = new StudentId(Guid.Parse("3E514647-E8B9-4EAB-7447-08D9DDDCD723"));
        var studySubjects = new HashSet<Subject>();
        var studyGrade = Grade.Tenth;

        // Act
        var action = () => { var studentProfile = new StudentProfile(studentId, studySubjects, studyGrade); };

        // Assert
        action.ShouldBrakeInvariant<StudentProfileMustHaveAtLeastOneStudySubjectInvariant>();
    }

    #endregion Constructor Tests

    #region AddStudySubjects Method Tests

    [Fact]
    public void AddStudySubjects_WhenThereAreNoNewStudySubjectsToBeAdded_ShouldNotModifyThePresentStudySubjects()
    {
        // Arrange
        var studentProfile = CreateDefaultStudentProfile();
        var initialStudySubjects = studentProfile.StudySubjects.ToList();
        var newStudySubjects = new HashSet<Subject>();

        // Act
        studentProfile.AddStudySubjects(newStudySubjects);

        // Assert
        studentProfile.StudySubjects.Should().BeEquivalentTo(initialStudySubjects);
    }

    [Fact]
    public void AddStudySubjects_WhenTheNewStudySubjectsToBeAddedAreAllAlreadyPresent_ShouldNotAddAnyNewStudySubjects()
    {
        // Arrange
        var studentProfile = CreateDefaultStudentProfile();
        var initialStudySubjects = studentProfile.StudySubjects.ToList();
        var newStudySubjects = initialStudySubjects.ToHashSet();

        // Act
        studentProfile.AddStudySubjects(newStudySubjects);

        // Assert
        studentProfile.StudySubjects.Should().BeEquivalentTo(initialStudySubjects);
    }

    [Theory, MemberData(nameof(StudySubjectsTestData))]
    public void AddStudySubjects_WhenThereAreNewStudySubjectsToBeAdded_ShouldAddOnlyTheNonAlreadyPresentStudySubjects(HashSet<Subject> newStudySubjects)
    {
        // Arrange
        var studentProfile = CreateDefaultStudentProfile();
        var initialStudySubjects = studentProfile.StudySubjects.ToList();
        var nonAlreadyPresentStudySubjects = newStudySubjects.Except(studentProfile.StudySubjects).ToList();

        // Act
        studentProfile.AddStudySubjects(newStudySubjects);

        // Assert
        studentProfile.StudySubjects.Should().BeEquivalentTo(initialStudySubjects.Union(nonAlreadyPresentStudySubjects));
    }

    [Theory, MemberData(nameof(StudySubjectsTestData))]
    public void AddStudySubjects_WhenThereAreNewStudySubjectsToBeAdded_ShouldAlwaysResultInUniqueStudySubjects(HashSet<Subject> newStudySubjects)
    {
        // Arrange
        var studentProfile = CreateDefaultStudentProfile();

        // Act
        studentProfile.AddStudySubjects(newStudySubjects);

        // Assert
        studentProfile.StudySubjects.Should().OnlyHaveUniqueItems();
    }

    #endregion AddStudySubjects Method Tests

    #region RemoveStudySubjects Method Tests

    [Fact]
    public void RemoveStudySubjects_WhenThereAreNoStudySubjectsForRemoval_ShouldNotModifyThePresentStudySubjects()
    {
        // Arrange
        var studentProfile = CreateDefaultStudentProfile();
        var initialStudySubjects = studentProfile.StudySubjects.ToList();
        var studySubjectsForRemoval = new HashSet<Subject>();

        // Act
        studentProfile.RemoveStudySubjects(studySubjectsForRemoval);

        // Assert
        studentProfile.StudySubjects.Should().BeEquivalentTo(initialStudySubjects);
    }

    [Theory, MemberData(nameof(StudySubjectsTestData))]
    public void RemoveStudySubjects_WhenThereAreStudySubjectsForRemoval_ShouldRemoveOnlyTheRequestedStudySubjectsForRemoval(HashSet<Subject> studySubjectsForRemoval)
    {
        // Arrange
        var studentProfile = CreateDefaultStudentProfile();
        var studySubjectsThatShouldNotBeRemoved = studentProfile.StudySubjects.Except(studySubjectsForRemoval).ToList();

        // Act
        studentProfile.RemoveStudySubjects(studySubjectsForRemoval);

        // Assert
        studentProfile.StudySubjects.Should().BeEquivalentTo(studySubjectsThatShouldNotBeRemoved);
    }

    [Fact]
    public void RemoveStudySubjects_WhenAllStudySubjectsAreToBeRemoved_ShouldBreakStudentProfileMustHaveAtLeastOneStudySubjectInvariant()
    {
        // Arrange
        var studentProfile = CreateDefaultStudentProfile();
        var studySubjectsForRemoval = studentProfile.StudySubjects.ToHashSet();

        // Act
        var action = () => studentProfile.RemoveStudySubjects(studySubjectsForRemoval);

        // Assert
        action.ShouldBrakeInvariant<StudentProfileMustHaveAtLeastOneStudySubjectInvariant>();
    }

    #endregion RemoveStudySubjects Method Tests

    #region UpdateStudyGrade Method Tests

    [Theory, MemberData(nameof(StudyGradeTestData))]
    public void UpdateStudyGrade_WhenGivenANewStudyGrade_ShouldUpdateTheStudyGradeToTheNewOne(Grade newStudyGrade)
    {
        // Arrange
        var studentProfile = CreateDefaultStudentProfile();

        // Act
        studentProfile.UpdateStudyGrade(newStudyGrade);

        // Assert
        studentProfile.StudyGrade.Should().BeEquivalentTo(newStudyGrade);
    }

    #endregion UpdateStudyGrade Method Tests

    #region Helper Methods

    private StudentProfile CreateDefaultStudentProfile() => new(DefaultStudentId, DefaultStudySubjects, DefaultStudyGrade);

    #endregion

    #region Test Data

    public static IEnumerable<object[]> StudySubjectsTestData => new List<object[]>
    {
        new object[] { new HashSet<Subject> { Subject.Math } },
        new object[] { new HashSet<Subject> { Subject.Math, Subject.Literature } },
        new object[] { new HashSet<Subject> { Subject.Literature, Subject.BulgarianAndLiterature } }
    };

    public static IEnumerable<object[]> StudyGradeTestData => new List<object[]>
    {
        new object[] { Grade.Forth },
        new object[] { Grade.Ninth },
        new object[] { Grade.Sixth }
    };

    #endregion
}
