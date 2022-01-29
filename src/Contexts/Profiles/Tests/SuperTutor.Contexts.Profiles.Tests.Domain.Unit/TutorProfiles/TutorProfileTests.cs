using FluentAssertions;
using SuperTutor.Contexts.Profiles.Domain.Common.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Constants;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Invariants;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Entities.RedactionComments.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.Enumerations;
using SuperTutor.Contexts.Profiles.Domain.TutorProfiles.Models.ValueObjects.Identifiers;
using SuperTutor.SharedLibraries.BuildingBlocks.Tests.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SuperTutor.Contexts.Profiles.Tests.Domain.Unit.TutorProfiles;

public class TutorProfileTests
{
    private readonly TutorId DefaultTutoId;
    private readonly string DefaultAbout;
    private readonly Subject DefaultTutoringSubject;
    private readonly HashSet<Grade> DefaultTutoringGrades;
    private readonly decimal DefaultRateForOneHour;
    private readonly AdminId DefaultAdminId;

    public TutorProfileTests()
    {
        DefaultTutoId = new TutorId(Guid.Parse("A9A60C0D-5405-4DB7-965A-E9168679C088"));
        DefaultAbout = "Tutor profile about.";
        DefaultTutoringSubject = Subject.Math;
        DefaultTutoringGrades = new HashSet<Grade> { Grade.Eleventh, Grade.Twelveth };
        DefaultRateForOneHour = 20m;
        DefaultAdminId = new AdminId(Guid.Parse("02C0DE38-A72D-4423-9F7B-E17CCDF4E626"));
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_WhenAllArgumentsAreProvided_ShouldCreateAValidInstance()
    {
        // Arrange

        // Act
        var tutorProfile = new TutorProfile(DefaultTutoId, DefaultAbout, DefaultTutoringSubject, DefaultTutoringGrades, DefaultRateForOneHour);

        // Assert
        tutorProfile.TutorId.Should().BeEquivalentTo(DefaultTutoId);
        tutorProfile.About.Should().BeEquivalentTo(DefaultAbout);
        tutorProfile.TutoringSubject.Should().BeEquivalentTo(DefaultTutoringSubject);
        tutorProfile.TutoringGrades.Should().BeEquivalentTo(DefaultTutoringGrades);
        tutorProfile.RateForOneHour.Should().Be(DefaultRateForOneHour);
        tutorProfile.Status.Should().BeEquivalentTo(TutorProfileStatus.ForReview);
    }

    [Fact]
    public void Constructor_WhenAboutIsEmpty_ShouldBreakTutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant()
    {
        // Arrange
        var about = "";

        // Act
        var action = () => { var tutorProfile = new TutorProfile(DefaultTutoId, about, DefaultTutoringSubject, DefaultTutoringGrades, DefaultRateForOneHour); };

        // Assert
        action.ShouldBrakeInvariant<TutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant>();
    }

    [Fact]
    public void Constructor_WhenAboutIsAboveMaxLenght_ShouldBreakTutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant()
    {
        // Arrange
        var about = new string('x', TutorProfileConstants.AboutMaxLength + 1);

        // Act
        var action = () => { var tutorProfile = new TutorProfile(DefaultTutoId, about, DefaultTutoringSubject, DefaultTutoringGrades, DefaultRateForOneHour); };

        // Assert
        action.ShouldBrakeInvariant<TutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant>();
    }

    [Fact]
    public void Constructor_WhenNoTutoringGradesAreProvided_ShouldBreakTutorProfileMustHaveAtLeastOneTutoringGradeInvariant()
    {
        // Arrange
        var tutoringGrades = new HashSet<Grade>();

        // Act
        var action = () => { var tutorProfile = new TutorProfile(DefaultTutoId, DefaultAbout, DefaultTutoringSubject, tutoringGrades, DefaultRateForOneHour); };

        // Assert
        action.ShouldBrakeInvariant<TutorProfileMustHaveAtLeastOneTutoringGradeInvariant>();
    }


    [Fact]
    public void Constructor_WhenTheRateForOneHourIsLessThanTheMinAmount_ShouldBreakTutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant()
    {
        // Arrange
        var rateForOneHour = TutorProfileConstants.RateForOneHourMinAmount - 1;

        // Act
        var action = () => { var tutorProfile = new TutorProfile(DefaultTutoId, DefaultAbout, DefaultTutoringSubject, DefaultTutoringGrades, rateForOneHour); };

        // Assert
        action.ShouldBrakeInvariant<TutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant>();
    }

    #endregion Constructor Tests

    #region Approve Method Tests

    [Fact]
    public void Approve_WhenTheTutorProfileIsNotMarkedForReview_ShouldBreakTutorProfileCanBeApprovedOnlyWhenItIsMarkedAsForReviewInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        tutorProfile.Deactivate();

        // Act
        var action = () => tutorProfile.Approve(DefaultAdminId);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileCanBeApprovedOnlyWhenItIsMarkedAsForReviewInvariant>();
    }

    [Fact]
    public void Approve_WhenTheTutorProfileIsMarkedForReview_ShouldTransitionTheTutorProfileToActive()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        // Act
        tutorProfile.Approve(DefaultAdminId);

        // Assert
        tutorProfile.Status.Should().Be(TutorProfileStatus.Active);
    }

    [Fact]
    public void Approve_WhenTheTutorProfileHasARedactionComment_ShouldSettleTheRedactionCommentWithApprovement()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);

        tutorProfile.RequestRedaction(redactionComment);
        tutorProfile.UpdateAbout("Changed about.");
        tutorProfile.SubmitForReview();

        // Act
        tutorProfile.Approve(DefaultAdminId);

        // Assert
        redactionComment.Status.Should().Be(TutorProfileRedactionCommentStatus.SettledWithApprovement);
    }

    #endregion Approve Method Tests

    #region RequestRedaction Method Tests

    [Fact]
    public void RequestRedaction_WhenTheTutorProfileIsNotMarkedForReview_ShouldBreakTutorProfileCanBeRedactionRequestedOnlyWhenItIsMarkedAsForReviewInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);
        tutorProfile.Deactivate();

        // Act
        var action = () => tutorProfile.RequestRedaction(redactionComment);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileCanBeRedactionRequestedOnlyWhenItIsMarkedAsForReviewInvariant>();
    }

    [Fact]
    public void RequestRedaction_WhenTheTutorProfileIsMarkedForReview_ShouldTransitionTheTutorProfileToActive()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);

        // Act
        tutorProfile.RequestRedaction(redactionComment);

        // Assert
        tutorProfile.Status.Should().Be(TutorProfileStatus.ForRedaction);
    }

    [Fact]
    public void RequestRedaction_WhenTheTutorProfileIsMarkedForReview_ShouldAddANewRedactionComment()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);

        // Act
        tutorProfile.RequestRedaction(redactionComment);

        // Assert
        tutorProfile.RedactionComments.Should().Contain(redactionComment);
    }

    [Fact]
    public void RequestRedaction_WhenTheTutorProfileHasARedactionComment_ShouldSettleTheRedactionCommentWithNewRedactionRequest()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var initialRedactionComment = CreateDefaultRedactionComment(tutorProfile.Id);
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);

        tutorProfile.RequestRedaction(initialRedactionComment);
        tutorProfile.UpdateAbout("Changed about.");
        tutorProfile.SubmitForReview();

        // Act
        tutorProfile.RequestRedaction(redactionComment);

        // Assert
        initialRedactionComment.Status.Should().Be(TutorProfileRedactionCommentStatus.SettledWithNewRedactionRequest);
    }

    #endregion RequestRedaction Method Tests

    #region SubmitForReview Method Tests

    [Fact]
    public void SubmitForReview_WhenTheTutorProfileIsNotMarkedAsInactiveOrForRedaction_ShouldBreakTutorProfileCanBeSubmittedForReviewOnlyWhenItIsMarkedAsInactiveOrForRedactionInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        tutorProfile.Approve(DefaultAdminId);

        // Act
        var action = () => tutorProfile.SubmitForReview();

        // Assert
        action.ShouldBrakeInvariant<TutorProfileCanBeSubmittedForReviewOnlyWhenItIsMarkedAsInactiveOrForRedactionInvariant>();
    }

    [Fact]
    public void SubmitForReview_WhenTheTutorProfileHasNotBeenModifiedSinceTheLastRedactionRequest_ShouldBreakTutorProfileCanBeSubmittedForReviewOnlyWhenItHasBeenModifiedSinceLastRedactionRequestInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);
        tutorProfile.RequestRedaction(redactionComment);

        // Act
        var action = () => tutorProfile.SubmitForReview();

        // Assert
        action.ShouldBrakeInvariant<TutorProfileCanBeSubmittedForReviewOnlyWhenItHasBeenModifiedSinceLastRedactionRequestInvariant>();
    }

    [Fact]
    public void SubmitForReview_WhenTheTutorProfileIsMarkedAsInactive_ShouldTransitionTheTutorProfileToForReview()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        tutorProfile.Deactivate();

        // Act
        tutorProfile.SubmitForReview();

        // Assert
        tutorProfile.Status.Should().Be(TutorProfileStatus.ForReview);
    }

    [Fact]
    public void SubmitForReview_WhenTheTutorProfileIsMarkedAsForRedaction_ShouldTransitionTheTutorProfileToForReview()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);

        tutorProfile.RequestRedaction(redactionComment);
        tutorProfile.UpdateAbout("Changed about.");

        // Act
        tutorProfile.SubmitForReview();

        // Assert
        tutorProfile.Status.Should().Be(TutorProfileStatus.ForReview);
    }

    #endregion SubmitForReview Method Tests

    #region Activate Method Tests

    [Fact]
    public void Activate_WhenTheTutorProfileIsNotMarkedAsInactive_ShouldBreakTutorProfileCanBeActivatedOnlyWhenItIsMarkedAsInactiveInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        // Act
        var action = () => tutorProfile.Activate();

        // Assert
        action.ShouldBrakeInvariant<TutorProfileCanBeActivatedOnlyWhenItIsMarkedAsInactiveInvariant>();
    }

    [Fact]
    public void Activate_WhenTheTutorProfileHasBeenModifiedSinceLastApproval_ShouldBreakTutorProfileCanBeActivatedOnlyWhenItHasNotBeenModifiedSinceLastApprovalInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();
        tutorProfile.UpdateAbout("Changed about.");

        // Act
        var action = () => tutorProfile.Activate();

        // Assert
        action.ShouldBrakeInvariant<TutorProfileCanBeActivatedOnlyWhenItHasNotBeenModifiedSinceLastApprovalInvariant>();
    }

    [Fact]
    public void Activate_WhenTheTutorProfileHasNeverBeenApproved_ShouldBreakTutorProfileCanNotBeActivatedWhenItHasNeverBeenApprovedInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        tutorProfile.Deactivate();

        // Act
        var action = () => tutorProfile.Activate();

        // Assert
        action.ShouldBrakeInvariant<TutorProfileCanNotBeActivatedWhenItHasNeverBeenApprovedInvariant>();
    }

    [Fact]
    public void Active_WhenTheTutorProfileIsMarkedAsInactive_ShouldTransitionTheTutorProfileToActive()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        tutorProfile.Activate();

        // Assert
        tutorProfile.Status.Should().Be(TutorProfileStatus.Active);
    }

    #endregion Activate Method Tests

    #region Deactivate Method Tests

    [Fact]
    public void Deactivate_WhenTheTutorProfileIsNotMarkedAsActiveOrForReview_ShouldBreakTutorProfileCanBeDeactivatedOnlyWhenItIsMarkedAsActiveOrForReviewInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);
        tutorProfile.RequestRedaction(redactionComment);

        // Act
        var action = () => tutorProfile.Deactivate();

        // Assert
        action.ShouldBrakeInvariant<TutorProfileCanBeDeactivatedOnlyWhenItIsMarkedAsActiveOrForReviewInvariant>();
    }

    [Fact]
    public void Deactivate_WhenTheTutorProfileIsMarkedAsActive_ShouldTransitionTheTutorProfileToInactive()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        tutorProfile.Approve(DefaultAdminId);

        // Act
        tutorProfile.Deactivate();

        // Assert
        tutorProfile.Status.Should().Be(TutorProfileStatus.Inactive);
    }

    [Fact]
    public void Deactivate_WhenTheTutorProfileIsMarkedAsForReview_ShouldTransitionTheTutorProfileToInactive()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        // Act
        tutorProfile.Deactivate();

        // Assert
        tutorProfile.Status.Should().Be(TutorProfileStatus.Inactive);
    }

    #endregion Deactivate Method Tests

    #region UpdateAbout Method Tests

    [Fact]
    public void UpdateAbout_WhenTheTutorProfileIsNotMarkedAsInactiveOrForRedaction_ShouldBreakTutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        // Act
        var action = () => tutorProfile.UpdateAbout("Changed about.");

        // Assert
        action.ShouldBrakeInvariant<TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant>();
    }

    [Fact]
    public void UpdateAbout_WhenTheNewAboutIsEmpty_ShouldBreakTutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        var action = () => tutorProfile.UpdateAbout("");

        // Assert
        action.ShouldBrakeInvariant<TutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant>();
    }

    [Fact]
    public void UpdateAbout_WhenTheNewAboutIsAboveTheMaxLenght_ShouldBreakTutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var newAbout = new string('x', TutorProfileConstants.AboutMaxLength + 1);

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        var action = () => tutorProfile.UpdateAbout(newAbout);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileAboutMustNotBeEmptyOrAboveTheMaxLenghtInvariant>();
    }

    [Fact]
    public void UpdateAbout_WhenTheTutorProfileIsMarkedAsInactive_ShouldUpdateTheAbout()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var newAbout = "Changed about.";

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        tutorProfile.UpdateAbout(newAbout);

        // Assert
        tutorProfile.About.Should().Be(newAbout);
    }

    [Fact]
    public void UpdateAbout_WhenTheTutorProfileIsMarkedAsForRedaction_ShouldUpdateTheAbout()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var newAbout = "Changed about.";
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);
        tutorProfile.RequestRedaction(redactionComment);

        // Act
        tutorProfile.UpdateAbout(newAbout);

        // Assert
        tutorProfile.About.Should().Be(newAbout);
    }

    #endregion UpdateAbout Method Tests

    #region AddTutoringGrades Method Tests

    [Fact]
    public void AddTutoringGrades_WhenTheTutorProfileIsNotMarkedAsInactiveOrForRedaction_ShouldBreakTutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var newTutoringGrades = new HashSet<Grade> { Grade.Ninth, Grade.Tenth };

        // Act
        var action = () => tutorProfile.AddTutoringGrades(newTutoringGrades);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant>();
    }

    [Fact]
    public void AddTutoringGrades_WhenThereAreNoNewTutoringGradesToBeAdded_ShouldNotModifyThePresentTutoringGrades()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var initialTutoringGrades = tutorProfile.TutoringGrades.ToList();
        var newTutoringGrades = new HashSet<Grade>();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        tutorProfile.AddTutoringGrades(newTutoringGrades);

        // Assert
        tutorProfile.TutoringGrades.Should().BeEquivalentTo(initialTutoringGrades);
    }

    [Fact]
    public void AddTutoringGrades_WhenTheNewTutoringGradesToBeAddedAreAllAlreadyPresent_ShouldNotAddAnyNewTutoringGrades()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var initialTutoringGrades = tutorProfile.TutoringGrades.ToList();
        var newTutoringGrades = initialTutoringGrades.ToHashSet();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        tutorProfile.AddTutoringGrades(newTutoringGrades);

        // Assert
        tutorProfile.TutoringGrades.Should().BeEquivalentTo(initialTutoringGrades);
    }

    [Theory, MemberData(nameof(TutoringGradesTestData))]
    public void AddTutoringGrades_WhenThereAreNewTutoringGradesToBeAddedAndTheTutorProfileIsMarkedAsInactive_ShouldAddOnlyTheNonAlreadyPresentTutoringGrades(HashSet<Grade> newTutoringGrades)
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var initialTutoringGrades = tutorProfile.TutoringGrades.ToList();
        var nonAlreadyPresentTutoringGrades = newTutoringGrades.Except(tutorProfile.TutoringGrades).ToList();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        tutorProfile.AddTutoringGrades(newTutoringGrades);

        // Assert
        tutorProfile.TutoringGrades.Should().BeEquivalentTo(initialTutoringGrades.Union(nonAlreadyPresentTutoringGrades));
    }

    [Theory, MemberData(nameof(TutoringGradesTestData))]
    public void AddTutoringGrades_WhenThereAreNewTutoringGradesToBeAddedAndTheTutorProfileIsMarkedAsForRedaction_ShouldAddOnlyTheNonAlreadyPresentTutoringGrades(HashSet<Grade> newTutoringGrades)
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var initialTutoringGrades = tutorProfile.TutoringGrades.ToList();
        var nonAlreadyPresentTutoringGrades = newTutoringGrades.Except(tutorProfile.TutoringGrades).ToList();
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);
        tutorProfile.RequestRedaction(redactionComment);

        // Act
        tutorProfile.AddTutoringGrades(newTutoringGrades);

        // Assert
        tutorProfile.TutoringGrades.Should().BeEquivalentTo(initialTutoringGrades.Union(nonAlreadyPresentTutoringGrades));
    }

    [Theory, MemberData(nameof(TutoringGradesTestData))]
    public void AddTutoringGrades_WhenThereAreNewTutoringGradesToBeAdded_ShouldAlwaysResultInUniqueTutoringGrades(HashSet<Grade> newTutoringGrades)
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        tutorProfile.AddTutoringGrades(newTutoringGrades);

        // Assert
        tutorProfile.TutoringGrades.Should().OnlyHaveUniqueItems();
    }

    #endregion AddTutoringGrades Method Tests

    #region RemoveTutoringGrades Method Tests

    [Fact]
    public void RemoveTutoringGrades_WhenTheTutorProfileIsNotMarkedAsInactiveOrForRedaction_ShouldBreakTutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var tutoringGradesForRemoval = new HashSet<Grade> { Grade.Ninth, Grade.Tenth };

        // Act
        var action = () => tutorProfile.RemoveTutoringGrades(tutoringGradesForRemoval);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant>();
    }

    [Fact]
    public void RemoveTutoringGrades_WhenThereAreNoTutoringGradesForRemoval_ShouldNotModifyThePresentTutoringGrades()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var initialTutoringGrades = tutorProfile.TutoringGrades.ToList();
        var tutoringGradesForRemoval = new HashSet<Grade>();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        tutorProfile.RemoveTutoringGrades(tutoringGradesForRemoval);

        // Assert
        tutorProfile.TutoringGrades.Should().BeEquivalentTo(initialTutoringGrades);
    }

    [Theory, MemberData(nameof(TutoringGradesTestData))]
    public void RemoveTutoringGrades_WhenThereAreTutoringGradesForRemovalAndTheTutorProfileIsMarkedAsInactive_ShouldRemoveOnlyTheRequestedTutoringGradesForRemoval(HashSet<Grade> tutoringGradesForRemoval)
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var tutoringGradesThatShouldNotBeRemoved = tutorProfile.TutoringGrades.Except(tutoringGradesForRemoval).ToList();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        tutorProfile.RemoveTutoringGrades(tutoringGradesForRemoval);

        // Assert
        tutorProfile.TutoringGrades.Should().BeEquivalentTo(tutoringGradesThatShouldNotBeRemoved);
    }

    [Theory, MemberData(nameof(TutoringGradesTestData))]
    public void RemoveTutoringGrades_WhenThereAreTutoringGradesForRemovalAndTheTutorProfileIsMarkedAsForRedaction_ShouldRemoveOnlyTheRequestedTutoringGradesForRemoval(HashSet<Grade> tutoringGradesForRemoval)
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var tutoringGradesThatShouldNotBeRemoved = tutorProfile.TutoringGrades.Except(tutoringGradesForRemoval).ToList();
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);
        tutorProfile.RequestRedaction(redactionComment);

        // Act
        tutorProfile.RemoveTutoringGrades(tutoringGradesForRemoval);

        // Assert
        tutorProfile.TutoringGrades.Should().BeEquivalentTo(tutoringGradesThatShouldNotBeRemoved);
    }

    [Fact]
    public void RemoveTutoringGrades_WhenAllTutoringGradesAreToBeRemoved_ShouldBreakTutorProfileMustHaveAtLeastOneTutoringGradeInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var tutoringGradesForRemoval = tutorProfile.TutoringGrades.ToHashSet();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        var action = () => tutorProfile.RemoveTutoringGrades(tutoringGradesForRemoval);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileMustHaveAtLeastOneTutoringGradeInvariant>();
    }

    #endregion RemoveTutoringGrades Method Tests

    #region IncreaseRateForOneHour Method Tests

    [Fact]
    public void IncreaseRateForOneHour_WhenTheTutorProfileIsNotMarkedAsInactiveOrForRedaction_ShouldBreakTutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        // Act
        var action = () => tutorProfile.IncreaseRateForOneHour(10m);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant>();
    }

    [Fact]
    public void IncreaseRateForOneHour_WhenTheIncreaseAmountIsLessThanOne_ShouldBreakTutorProfileRateForOneHourCanOnlyBeModifiedWithAmountsAboveZeroInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        var action = () => tutorProfile.IncreaseRateForOneHour(-1m);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileRateForOneHourCanOnlyBeModifiedWithAmountsAboveZeroInvariant>();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(5)]
    [InlineData(100)]
    public void IncreaseRateForOneHour_WhenTheTutorProfileIsMarkedAsInactive_ShouldIncreaseTheRateForOneHourWithTheIncreaseAmount(decimal increaseAmount)
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var initialRateForOneHour = tutorProfile.RateForOneHour;

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        tutorProfile.IncreaseRateForOneHour(increaseAmount);

        // Assert
        tutorProfile.RateForOneHour.Should().Be(initialRateForOneHour + increaseAmount);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(5)]
    [InlineData(100)]
    public void IncreaseRateForOneHour_WhenTheTutorProfileIsMarkedAsForRedaction_ShouldIncreaseTheRateForOneHourWithTheIncreaseAmount(decimal increaseAmount)
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var initialRateForOneHour = tutorProfile.RateForOneHour;
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);
        tutorProfile.RequestRedaction(redactionComment);

        // Act
        tutorProfile.IncreaseRateForOneHour(increaseAmount);

        // Assert
        tutorProfile.RateForOneHour.Should().Be(initialRateForOneHour + increaseAmount);
    }

    #endregion IncreaseRateForOneHour Method Tests

    #region DecreaseRateForOneHour Method Tests

    [Fact]
    public void DecreaseRateForOneHour_WhenTheTutorProfileIsNotMarkedAsInactiveOrForRedaction_ShouldBreakTutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        // Act
        var action = () => tutorProfile.DecreaseRateForOneHour(10m);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileInformationCanOnlyBeUpdatedWhenItIsMarkedAsInactiveOrForRedactionInvariant>();
    }

    [Fact]
    public void DecreaseRateForOneHour_WhenTheDecreaseAmountIsLessThanOne_ShouldBreakTutorProfileRateForOneHourCanOnlyBeModifiedWithAmountsAboveZeroInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        var action = () => tutorProfile.DecreaseRateForOneHour(-1m);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileRateForOneHourCanOnlyBeModifiedWithAmountsAboveZeroInvariant>();
    }

    [Fact]
    public void DecreaseRateForOneHour_WhenTheResultingRateForOneHourIsLessThanTheMinAllowedAmount_ShouldTutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant()
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var decreaseAmount = tutorProfile.RateForOneHour - TutorProfileConstants.RateForOneHourMinAmount + 1;

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        var action = () => tutorProfile.DecreaseRateForOneHour(decreaseAmount);

        // Assert
        action.ShouldBrakeInvariant<TutorProfileRateForOneHourMustNotBeLessThanTheMinAmountInvariant>();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(5)]
    [InlineData(3)]
    public void DecreaseRateForOneHour_WhenTheTutorProfileIsMarkedAsInactive_ShouldDecreaseTheRateForOneHourWithTheDecreaseAmount(decimal decreaseAmount)
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var initialRateForOneHour = tutorProfile.RateForOneHour;

        tutorProfile.Approve(DefaultAdminId);
        tutorProfile.Deactivate();

        // Act
        tutorProfile.DecreaseRateForOneHour(decreaseAmount);

        // Assert
        tutorProfile.RateForOneHour.Should().Be(initialRateForOneHour - decreaseAmount);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(5)]
    [InlineData(3)]
    public void DecreaseRateForOneHour_WhenTheTutorProfileIsMarkedAsForRedaction_ShouldDecreaseTheRateForOneHourWithTheDecreaseAmount(decimal decreaseAmount)
    {
        // Arrange
        var tutorProfile = CreateDefaultTutorProfile();
        var initialRateForOneHour = tutorProfile.RateForOneHour;
        var redactionComment = CreateDefaultRedactionComment(tutorProfile.Id);
        tutorProfile.RequestRedaction(redactionComment);

        // Act
        tutorProfile.DecreaseRateForOneHour(decreaseAmount);

        // Assert
        tutorProfile.RateForOneHour.Should().Be(initialRateForOneHour - decreaseAmount);
    }

    #endregion DecreaseRateForOneHour Method Tests

    #region Helper Methods

    private TutorProfile CreateDefaultTutorProfile()
        => new TutorProfile(DefaultTutoId, DefaultAbout, DefaultTutoringSubject, DefaultTutoringGrades, DefaultRateForOneHour);

    private TutorProfileRedactionComment CreateDefaultRedactionComment(TutorProfileId tutorProfileId)
        => new TutorProfileRedactionComment(tutorProfileId, DefaultAdminId, "Change about text.");

    #endregion Helper Methods

    #region Test Data

    public static IEnumerable<object[]> TutoringGradesTestData = new List<object[]>
    {
        new object[] { new HashSet<Grade> { Grade.Twelveth } },
        new object[] { new HashSet<Grade> { Grade.Twelveth, Grade.Tenth } },
        new object[] { new HashSet<Grade> { Grade.Tenth, Grade.Ninth } }
    };

    #endregion
}
