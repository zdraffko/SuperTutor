using Dapper;
using FluentAssertions;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Shared;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Shared.Models;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Commands.AddFavoriteFilter.Models;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Shared;
using System.Data.SqlClient;
using System.Net.Http.Json;
using TechTalk.SpecFlow;

namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.Students.Commands.AddFavoriteFilter;

[Binding]
public class AddFavoriteFilterStepDefinitions
{
    private const string StudentId = "A90A5DC7-03A7-48D4-A029-72F222698AAF";
    private const string NewFilter = "&newFilter=true";
    private const string GetAddedFavoriteFilterQuery = "select * from catalog.FavoriteFilters where StudentId = @StudentId and Filter = @NewFilter";

    private readonly HttpClient httpClient;
    private readonly AddFavoriteFilterForStudentRequest addNewFavoriteFilterRequest;
    private readonly object getAddedFavoriteFilterQueryParameters;

    public AddFavoriteFilterStepDefinitions(HttpClient httpClient)
    {
        this.httpClient = httpClient;

        addNewFavoriteFilterRequest = new AddFavoriteFilterForStudentRequest
        {
            StudentId = StudentId,
            Filter = NewFilter
        };

        getAddedFavoriteFilterQueryParameters = new
        {
            StudentId,
            NewFilter
        };
    }

    [Given(@"Alex is a student")]
    public async Task GivenAlexIsAStudent()
    {
        using var connection = new SqlConnection(Constants.DatabaseConnectionString);
        await connection.ExecuteAsync("insert into catalog.Students (Id) values (@StudentId)", new { StudentId });
    }

    [Given(@"Alex has the maximum number of filters in his favorites")]
    public async Task GivenTheStudentHasTheMaximumNumberOfFiltersInHisFavorites() => await AddFavoriteFilters(Domain.Students.Constants.StudentConstants.MaximumAllowedFavoriteFilters);

    [Given(@"the same filter is already present in Alex's favorites")]
    public async Task GivenTheSameFilterIsAlreadyPresentInTheStudentsFavorites() => await httpClient.PostAsJsonAsync(StudentConstants.AddFavoriteFilterEndpoint, addNewFavoriteFilterRequest);

    [Given(@"the same filter is not already present in Alex's favorites")]
    public async Task GivenTheSameFilterIsNotAlreadyPresentInTheStudentsFavorites()
    {
        using var connection = new SqlConnection(Constants.DatabaseConnectionString);
        await connection.ExecuteAsync("delete catalog.FavoriteFilters where StudentId = @StudentId and Filter = @NewFilter", getAddedFavoriteFilterQueryParameters);
    }

    [Given(@"Alex has not reached the maximum number of allowed favorite filters \(he has '([^']*)'\)")]
    public async Task GivenTheStudentHasNotReachedTheMaximumNumberOfAllowedFavoriteFiltersHeHas(int numberOfAlreadyAddedFavoriteFilters) => await AddFavoriteFilters(numberOfAlreadyAddedFavoriteFilters);

    [When(@"Alex tries add a new filter")]
    public async Task WhenTheStudentTriesAddANewFilter() => await httpClient.PostAsJsonAsync(StudentConstants.AddFavoriteFilterEndpoint, addNewFavoriteFilterRequest);

    [Then(@"the new filter should not be added to Alex's favorites")]
    public async Task ThenTheNewFilterShouldNotBeAddedToTheStudentsFavorites()
    {
        using var connection = new SqlConnection(Constants.DatabaseConnectionString);
        var favoriteFilters = await connection.QueryAsync<FavoriteFilterDatabaseQueryResponse>(GetAddedFavoriteFilterQuery, getAddedFavoriteFilterQueryParameters);

        favoriteFilters.Should().NotContain(favoriteFilter => favoriteFilter.StudentId!.Value.ToString().ToUpper() == StudentId && favoriteFilter.Filter == NewFilter);
    }

    [Then(@"the new filter should not be added for a second time to Alex's favorites")]
    public async Task ThenTheNewFilterShouldNotBeAddedASecondTimeToTheStudentsFavorites()
    {
        using var connection = new SqlConnection(Constants.DatabaseConnectionString);
        var favoriteFilters = await connection.QueryAsync<FavoriteFilterDatabaseQueryResponse>(GetAddedFavoriteFilterQuery, getAddedFavoriteFilterQueryParameters);

        favoriteFilters.Should().ContainSingle(favoriteFilter => favoriteFilter.StudentId!.Value.ToString().ToUpper() == StudentId && favoriteFilter.Filter == NewFilter);
    }

    [Then(@"the number of Alex's favorite filters should remain at the maximum")]
    public async Task ThenTheNumberOfTheStudentsFavoriteFiltersShouldRemainAtTheMaximum()
    {
        using var connection = new SqlConnection(Constants.DatabaseConnectionString);
        var favoriteFilters = await connection.QueryAsync<FavoriteFilterDatabaseQueryResponse>("select * from catalog.FavoriteFilters where StudentId = @StudentId", new { StudentId });

        favoriteFilters.Should().HaveCount(Domain.Students.Constants.StudentConstants.MaximumAllowedFavoriteFilters);
    }

    [Then(@"the new filter should be added to Alex's favorites")]
    public async Task ThenTheNewFilterShouldBeAddedToTheStudentsFavorites()
    {
        using var connection = new SqlConnection(Constants.DatabaseConnectionString);
        var favoriteFilters = await connection.QueryAsync<FavoriteFilterDatabaseQueryResponse>(GetAddedFavoriteFilterQuery, getAddedFavoriteFilterQueryParameters);

        favoriteFilters.Should().HaveCount(1);
        favoriteFilters.Single().StudentId!.Value.ToString().ToUpper().Should().Be(StudentId);
        favoriteFilters.Single().Filter.Should().Be(NewFilter);
    }

    private async Task AddFavoriteFilters(int numberOfFavoriteFiltersToAdd)
    {
        var addFavoriteFilterRequestTasks = new List<Task<HttpResponseMessage>>();
        for (var addFavoriteFilterRequestNumber = 1; addFavoriteFilterRequestNumber <= numberOfFavoriteFiltersToAdd; addFavoriteFilterRequestNumber++)
        {
            var addFavoriteFilterRequest = new AddFavoriteFilterForStudentRequest
            {
                StudentId = StudentId,
                Filter = $"&favoriteFilterNumber={addFavoriteFilterRequestNumber}"
            };

            var addFavoriteFilterRequestTask = httpClient.PostAsJsonAsync(StudentConstants.AddFavoriteFilterEndpoint, addFavoriteFilterRequest);

            addFavoriteFilterRequestTasks.Add(addFavoriteFilterRequestTask);
        }

        await Task.WhenAll(addFavoriteFilterRequestTasks);
    }
}
