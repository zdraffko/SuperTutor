using FluentAssertions;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.Contexts.Catalog.Domain.Students.Constants;
using SuperTutor.Contexts.Catalog.Domain.Students.Invariants;
using SuperTutor.Contexts.Catalog.Domain.Students.Models.ValueObjects.FavoriteFilters;
using SuperTutor.SharedLibraries.BuildingBlocks.Tests.Domain.Extensions;
using System;
using Xunit;

namespace SuperTutor.Contexts.Catalog.Tests.Unit.Domain.Students;

public class StudentTests
{
    private readonly StudentId studentId = new(Guid.Parse("A90A5DC7-03A7-48D4-A029-72F222698AAF"));

    #region Add Method Tests

    [Fact]
    public void AddFavoriteFilter_WhenTheNewFilterIsUniqueAndTheStudentHasNotReachedTheMaximumNumberOfFavoriteFilters_ShouldAddTheNewFilterToTheStudentsFavorites()
    {
        // Arrange
        var student = new Student(studentId);
        var newFavoriteFilter = new FavoriteFilter(student.Id, "&newFilter=true");

        // Act
        student.AddFavoriteFilter(newFavoriteFilter);

        // Assert
        student.FavoriteFilters.Should().Contain(newFavoriteFilter);
    }

    [Fact]
    public void AddFavoriteFilter_WhenTheFilterIsNotUniqueForTheStudent_ShouldBreakFavoriteFilterMustBeUniqueForStudentInvariant()
    {
        // Arrange
        var student = new Student(studentId);
        var newFavoriteFilter = new FavoriteFilter(student.Id, "&newFilter=true");
        student.AddFavoriteFilter(newFavoriteFilter);

        // Act
        var action = () => student.AddFavoriteFilter(newFavoriteFilter);

        // Assert
        action.ShouldBrakeInvariant<StudentMustHaveOnlyUniqueFavoriteFiltersInvariant>();
    }

    [Fact]
    public void AddFavoriteFilter_WhenTheStudentAlreadyHasTheMaximumNumberOfFavoriteFilters_ShouldBreakStudentCanNotHaveMoreThanTheMaximumAllowedFavoriteFiltersInvariant()
    {
        // Arrange
        var student = CreateStudentWithMaximumNumberOfFavoriteFilters(StudentConstants.MaximumAllowedFavoriteFilters);
        var newFavoriteFilter = new FavoriteFilter(student.Id, "&newFilter=true");

        // Act
        var action = () => student.AddFavoriteFilter(newFavoriteFilter);

        // Assert
        action.ShouldBrakeInvariant<StudentCanNotHaveMoreThanTheMaximumAllowedFavoriteFiltersInvariant>();
    }

    #endregion Add Method Tests

    #region Add Method Tests

    [Fact]
    public void RemoveFavoriteFilter_WhenTheFilterForRemovalIsPresentInTheStudentsFavorites_ShouldRemoveTheFilterFromTheStudentsFavorites()
    {
        // Arrange
        var student = new Student(studentId);
        var presentFavoriteFilterOne = new FavoriteFilter(student.Id, "&filter=1");
        var presentFavoriteFilterTwo = new FavoriteFilter(student.Id, "&filter=2");

        student.AddFavoriteFilter(presentFavoriteFilterOne);
        student.AddFavoriteFilter(presentFavoriteFilterTwo);

        // Act
        student.RemoveFavoriteFilter(presentFavoriteFilterOne.Filter);

        // Assert
        student.FavoriteFilters
            .Should().HaveCount(1)
            .And.NotContain(presentFavoriteFilterOne);
    }

    [Fact]
    public void RemoveFavoriteFilter_WhenTheFilterForRemovalIsNotPresentInTheStudentsFavorites_ShouldNotModifyTheStudentsFavoriteFilters()
    {
        // Arrange
        var student = new Student(studentId);
        var presentFavoriteFilterOne = new FavoriteFilter(student.Id, "&filter=1");
        var presentFavoriteFilterTwo = new FavoriteFilter(student.Id, "&filter=2");

        student.AddFavoriteFilter(presentFavoriteFilterOne);
        student.AddFavoriteFilter(presentFavoriteFilterTwo);

        // Act
        student.RemoveFavoriteFilter("&nonPresentFilter=true");

        // Assert
        student.FavoriteFilters.Should().HaveCount(2);
    }

    #endregion Add Method Tests

    #region Helper Methods

    private Student CreateStudentWithMaximumNumberOfFavoriteFilters(int maximumNumberOfFavoriteFilters)
    {
        var student = new Student(studentId);

        for (var favoriteFilterNumber = 1; favoriteFilterNumber <= maximumNumberOfFavoriteFilters; favoriteFilterNumber++)
        {
            var favoriteFilter = new FavoriteFilter(student.Id, $"&filterNumber={favoriteFilterNumber}");
            student.AddFavoriteFilter(favoriteFilter);
        }

        return student;
    }

    #endregion Helper Methods
}
