using TechTalk.SpecFlow;

namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.FavoriteFilters.Commands.Add;

[Binding]
public class AddFavoriteFilterStepDefinitions
{
    [Given(@"the student has the maximum number of filters in his favorites")]
    public void GivenTheStudentHasTheMaximumNumberOfFiltersInHisFavorites() => throw new PendingStepException();

    [When(@"the student tries add a new filter")]
    public void WhenTheStudentTriesAddANewFilter() => throw new PendingStepException();

    [Then(@"the new filter should not be added to the student's favorites")]
    public void ThenTheNewFilterShouldNotBeAddedToTheStudentsFavorites() => throw new PendingStepException();

    [Given(@"the same filter is already present in the student's favorites")]
    public void GivenTheSameFilterIsAlreadyPresentInTheStudentsFavorites() => throw new PendingStepException();

    [Given(@"the same filter is not already present in the student's favorites")]
    public void GivenTheSameFilterIsNotAlreadyPresentInTheStudentsFavorites() => throw new PendingStepException();

    [Given(@"the student has not reached the maximum number of allowed favorite filters")]
    public void GivenTheStudentHasNotReachedTheMaximumNumberOfAllowedFavoriteFilters() => throw new PendingStepException();

    [Then(@"the new filter should be added to the student's favorites")]
    public void ThenTheNewFilterShouldBeAddedToTheStudentsFavorites() => throw new PendingStepException();
}
