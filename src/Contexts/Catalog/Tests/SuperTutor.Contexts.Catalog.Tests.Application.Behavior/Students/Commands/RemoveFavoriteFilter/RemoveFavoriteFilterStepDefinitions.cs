using Dapper;
using FluentAssertions;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Shared;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Shared.Models;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Commands.AddFavoriteFilter.Models;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Commands.RemoveFavoriteFilter.Models;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Shared;
using System.Data.SqlClient;
using System.Net.Http.Json;
using TechTalk.SpecFlow;

namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Commands.Remove;

[Binding]
public class RemoveFavoriteFilterStepDefinitions
{
    private const string StudentId = "A90A5DC7-03A7-48D4-A029-72F222698AAF";
    private const string ExistingFilter = "&newFilter=true";

    private readonly HttpClient httpClient;

    public RemoveFavoriteFilterStepDefinitions(HttpClient httpClient) => this.httpClient = httpClient;

    [Given(@"Alex has a filter in his favorites")]
    public async Task GivenAlexHasAFilterInHisFavorites()
    {
        var addNewFavoriteFilterRequest = new AddFavoriteFilterForStudentRequest
        {
            StudentId = StudentId,
            Filter = ExistingFilter
        };

        await httpClient.PostAsJsonAsync(StudentConstants.AddFavoriteFilterEndpoint, addNewFavoriteFilterRequest);
    }

    [When(@"Alex tries to remove that filter")]
    public async Task WhenAlexTriesToRemoveThatFilter()
    {
        var removeFavoriteFilterRequest = new RemoveFavoriteFilterForStudentRequest
        {
            StudentId = StudentId,
            Filter = ExistingFilter
        };

        await httpClient.PostAsJsonAsync(StudentConstants.RemoveFavoriteFilterEndpoint, removeFavoriteFilterRequest);
    }

    [When(@"Alex tries to remove a non existing filter from his favorites")]
    public async Task WhenAlexTriesToRemoveANonExistingFilterFromHis()
    {
        var removeFavoriteFilterRequest = new RemoveFavoriteFilterForStudentRequest
        {
            StudentId = StudentId,
            Filter = "&nonExistingFilter=true"
        };

        await httpClient.PostAsJsonAsync(StudentConstants.RemoveFavoriteFilterEndpoint, removeFavoriteFilterRequest);
    }

    [Then(@"the filter should be removed from his favorites")]
    public async Task ThenTheFilterShouldBeRemovedFromHisFavorites()
    {
        using var connection = new SqlConnection(Constants.DatabaseConnectionString);
        var favoriteFilters = await connection.QueryAsync<FavoriteFilterDatabaseQueryResponse>("select * from catalog.FavoriteFilters where StudentId = @StudentId", new { StudentId });

        favoriteFilters.Should().NotContain(favoriteFilter => favoriteFilter.Filter == ExistingFilter);
    }

    [Then(@"non of his filters should be removed")]
    public async Task ThenNonOfHisFiltersShouldBeRemoved()
    {
        using var connection = new SqlConnection(Constants.DatabaseConnectionString);
        var favoriteFilters = await connection.QueryAsync<FavoriteFilterDatabaseQueryResponse>("select * from catalog.FavoriteFilters where StudentId = @StudentId", new { StudentId });

        favoriteFilters.Should().HaveCount(1);
    }
}
