﻿using FluentAssertions;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Constants;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Invariants;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Tests.Domain.Extensions;
using System;
using Xunit;

namespace SuperTutor.Contexts.Profiles.Tests.Domain.Unit.TutorProfiles.Models.Entities.RedactionComments;

public class TutorProfileRedactionCommentTests
{
    private readonly TutorProfileId DefaultTutorProfileId;
    private readonly AdminId DefaultAdminId;
    private readonly string DefaultContent;

    public TutorProfileRedactionCommentTests()
    {
        DefaultTutorProfileId = new TutorProfileId(Guid.Parse("A9A60C0D-5405-4DB7-965A-E9168679C088"));
        DefaultAdminId = new AdminId(Guid.Parse("02C0DE38-A72D-4423-9F7B-E17CCDF4E626"));
        DefaultContent = "Please change about.";
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_WhenAllArgumentsAreProvided_ShouldCreateAValidInstance()
    {
        // Arrange

        // Act
        var redactionComment = new TutorProfileRedactionComment(DefaultTutorProfileId, DefaultAdminId, DefaultContent);

        // Assert
        redactionComment.TutorProfileId.Should().BeEquivalentTo(DefaultTutorProfileId);
        redactionComment.CreatedByAdminId.Should().BeEquivalentTo(DefaultAdminId);
        redactionComment.Content.Should().BeEquivalentTo(DefaultContent);
        redactionComment.Status.Should().BeEquivalentTo(TutorProfileRedactionCommentStatus.Active);
    }

    [Fact]
    public void Constructor_WhenContentIsEmpty_ShouldBreakTutorProfileRedactionCommentContentMustNotBeEmptyOrAboveTheMaxLenghtInvariant()
    {
        // Arrange
        var content = "";

        // Act
        var action = () => { var redactionComment = new TutorProfileRedactionComment(DefaultTutorProfileId, DefaultAdminId, content); };

        // Assert
        action.ShouldBrakeInvariant<TutorProfileRedactionCommentContentMustNotBeEmptyOrAboveTheMaxLenghtInvariant>();
    }

    [Fact]
    public void Constructor_WhenContentIsAboveMaxLenght_ShouldBreakTutorProfileRedactionCommentContentMustNotBeEmptyOrAboveTheMaxLenghtInvariant()
    {
        // Arrange
        var content = new string('x', TutorProfileRedactionCommentConstants.ContentMaxLength + 1);

        // Act
        var action = () => { var redactionComment = new TutorProfileRedactionComment(DefaultTutorProfileId, DefaultAdminId, content); };

        // Assert
        action.ShouldBrakeInvariant<TutorProfileRedactionCommentContentMustNotBeEmptyOrAboveTheMaxLenghtInvariant>();
    }

    #endregion Constructor Tests

    #region SettleWithApprovement Tests

    [Fact]
    public void SettleWithApprovement_WhenTheRedactionCommentIsAlreadySettled_ShouldBreakTutorProfileRedactionCommentCannotBeSettledMoreThanOnceInvariant()
    {
        // Arrange
        var redactionComment = new TutorProfileRedactionComment(DefaultTutorProfileId, DefaultAdminId, DefaultContent);
        redactionComment.SettleWithNewRedactionRequest(DefaultAdminId);

        // Act
        var action = () => redactionComment.SettleWithApprovement(DefaultAdminId);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileRedactionCommentCannotBeSettledMoreThanOnceInvariant>();
    }

    [Fact]
    public void SettleWithApprovement_WhenTheRedactionCommentIsNotAlreadySettled_ShouldMarkTheRedactionCommentAsSettledWithApprovement()
    {
        // Arrange
        var redactionComment = new TutorProfileRedactionComment(DefaultTutorProfileId, DefaultAdminId, DefaultContent);

        // Act
        redactionComment.SettleWithApprovement(DefaultAdminId);

        // Assert
        redactionComment.Status.Should().BeEquivalentTo(TutorProfileRedactionCommentStatus.SettledWithApprovement);
    }

    #endregion SettleWithApprovement Tests

    #region SettleWithNewRedactionRequest Tests

    [Fact]
    public void SettleWithNewRedactionRequest_WhenTheRedactionCommentIsAlreadySettled_ShouldBreakTutorProfileRedactionCommentCannotBeSettledMoreThanOnceInvariant()
    {
        // Arrange
        var redactionComment = new TutorProfileRedactionComment(DefaultTutorProfileId, DefaultAdminId, DefaultContent);
        redactionComment.SettleWithApprovement(DefaultAdminId);

        // Act
        var action = () => redactionComment.SettleWithNewRedactionRequest(DefaultAdminId);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileRedactionCommentCannotBeSettledMoreThanOnceInvariant>();
    }

    [Fact]
    public void SettleWithNewRedactionRequest_WhenTheRedactionCommentIsNotAlreadySettled_ShouldMarkTheRedactionCommentAsSettledWithRedactionRequest()
    {
        // Arrange
        var redactionComment = new TutorProfileRedactionComment(DefaultTutorProfileId, DefaultAdminId, DefaultContent);

        // Act
        redactionComment.SettleWithNewRedactionRequest(DefaultAdminId);

        // Assert
        redactionComment.Status.Should().BeEquivalentTo(TutorProfileRedactionCommentStatus.SettledWithNewRedactionRequest);
    }

    #endregion SettleWithNewRedactionRequest Tests

    #region Helper Methods

    private TutorProfileRedactionComment CreateDefaultRedactionComment()
        => new TutorProfileRedactionComment(DefaultTutorProfileId, DefaultAdminId, DefaultContent);

    #endregion Helper Methods
}