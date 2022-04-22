using FluentAssertions;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Shared;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Commands.AddFavoriteFilter.Models;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Queries.GetAllfavoriteFilters.Models;
using System.Net.Http.Json;
using TechTalk.SpecFlow;

namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Queries.GetAll;

[Binding]
public class GetAllFavoriteFiltersStepDefinitions
{
    private const string StudentId = "A90A5DC7-03A7-48D4-A029-72F222698AAF";
    private const string ExistingFilter = "&newFilter=true";

    private readonly HttpClient httpClient;

    private IEnumerable<GetFavoriteFiltersForStudentResponse>? favoriteFilters;

    public GetAllFavoriteFiltersStepDefinitions(HttpClient httpClient) => this.httpClient = httpClient;

    [Given(@"Alex has favorite filters")]
    public async Task GivenAlexHasFavoriteFilters()
    {
        var addNewFavoriteFilterRequest = new AddFavoriteFilterForStudentRequest
        {
            StudentId = StudentId,
            Filter = ExistingFilter
        };

        await httpClient.PostAsJsonAsync(Constants.AddFavoriteFilterEndpoint, addNewFavoriteFilterRequest);
    }

    [Given(@"Alex does not have any favorite filters")]
    public void GivenAlexDoesNotHaveAnyFavoriteFilters() { }

    [When(@"Alex tries to get all of his favorite filters")]
    public async Task WhenAlexTriesToGetAllOfHisFavoriteFilters() => favoriteFilters = await httpClient.GetFromJsonAsync<IEnumerable<GetFavoriteFiltersForStudentResponse>>($"{Constants.GetAllFavoriteFiltersEndpoint}?StudentId={StudentId}");

    [Then(@"all of Alex's favorite filters should be returned")]
    public void AllOfAlexsFavoriteFiltersShouldBeReturned() => favoriteFilters
        .Should().HaveCount(1)
        .And.Contain(favoriteFilter => favoriteFilter.Filter == ExistingFilter);

    [Then(@"no favorite filters should be returned")]
    public void ThenNoFavoriteFiltersShouldBeReturned() => favoriteFilters
        .Should().HaveCount(0);
}
