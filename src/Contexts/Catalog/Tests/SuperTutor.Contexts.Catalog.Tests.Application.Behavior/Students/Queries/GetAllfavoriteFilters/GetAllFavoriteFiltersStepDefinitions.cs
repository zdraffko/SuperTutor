using FluentAssertions;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Commands.AddFavoriteFilter.Models;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Queries.GetAllfavoriteFilters.Models;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Shared;
using System.Net.Http.Json;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Queries.GetAll;

[Binding]
public class GetAllFavoriteFiltersStepDefinitions
{
    private const string StudentId = "A90A5DC7-03A7-48D4-A029-72F222698AAF";
    private const string ExistingFilter = "&newFilter=true";
    private readonly HttpClient httpClient;
    private GetFavoriteFiltersForStudentResponse? getFavoriteFiltersResponse;

    public GetAllFavoriteFiltersStepDefinitions(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        getFavoriteFiltersResponse = new GetFavoriteFiltersForStudentResponse();
    }

    [Given(@"Alex has favorite filters")]
    public async Task GivenAlexHasFavoriteFilters()
    {
        var addNewFavoriteFilterRequest = new AddFavoriteFilterForStudentRequest
        {
            StudentId = StudentId,
            Filter = ExistingFilter
        };

        await httpClient.PostAsJsonAsync(StudentConstants.AddFavoriteFilterEndpoint, addNewFavoriteFilterRequest);
    }

    [Given(@"Alex does not have any favorite filters")]
    public void GivenAlexDoesNotHaveAnyFavoriteFilters() { }

    [When(@"Alex tries to get all of his favorite filters")]
    public async Task WhenAlexTriesToGetAllOfHisFavoriteFilters()
    {
        var request = JsonSerializer.Serialize(new { StudentId });

        getFavoriteFiltersResponse = await httpClient.GetFromJsonAsync<GetFavoriteFiltersForStudentResponse>($"{StudentConstants.GetAllFavoriteFiltersEndpoint}?query={request}");
    }

    [Then(@"all of Alex's favorite filters should be returned")]
    public void AllOfAlexsFavoriteFiltersShouldBeReturned() => getFavoriteFiltersResponse?.Filters
        .Should().HaveCount(1)
        .And.Contain(filter => filter == ExistingFilter);

    [Then(@"no favorite filters should be returned")]
    public void ThenNoFavoriteFiltersShouldBeReturned() => getFavoriteFiltersResponse?.Filters
        .Should().HaveCount(0);
}
