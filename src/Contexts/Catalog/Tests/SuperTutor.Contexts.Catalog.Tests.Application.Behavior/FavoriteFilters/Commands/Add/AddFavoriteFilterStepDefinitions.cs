using Dapper;
using FluentAssertions;
using SuperTutor.Contexts.Catalog.Tests.Application.Behavior.FavoriteFilters.Commands.Add.Models;
using System.Data.SqlClient;
using System.Net.Http.Json;
using TechTalk.SpecFlow;

namespace SuperTutor.Contexts.Catalog.Tests.Application.Behavior.FavoriteFilters.Commands.Add;

[Binding]
public class AddFavoriteFilterStepDefinitions
{
    private const string DatabaseConnectionString = "Server=localhost;Database=SuperTutorCatalogTest;User Id=sa;Password=testPass123;MultipleActiveResultSets=true";
    private const string AddFavoriteFilterEndpoint = "/api/FavoriteFilters/Add";
    private const string StudentId = "A90A5DC7-03A7-48D4-A029-72F222698AAF";
    private const string NewFilter = "&newFilter=true";
    private const int MaximumNumberOfFiltersForAStudent = 3;
    private const string GetAddedFavoriteFilterQuery = "select * from catalog.FavoriteFilters where StudentId = @StudentId and Filter = @NewFilter";

    private readonly HttpClient httpClient;
    private readonly AddFavoriteFilterRequest addNewFavoriteFilterRequest;
    private readonly object getAddedFavoriteFilterQueryParameters;

    public AddFavoriteFilterStepDefinitions(HttpClient httpClient)
    {
        this.httpClient = httpClient;

        addNewFavoriteFilterRequest = new AddFavoriteFilterRequest
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

    [Given(@"the student has the maximum number of filters in his favorites")]
    public async Task GivenTheStudentHasTheMaximumNumberOfFiltersInHisFavorites() => await AddFavoriteFilters(MaximumNumberOfFiltersForAStudent);

    [Given(@"the same filter is already present in the student's favorites")]
    public async Task GivenTheSameFilterIsAlreadyPresentInTheStudentsFavorites() => await httpClient.PostAsJsonAsync(AddFavoriteFilterEndpoint, addNewFavoriteFilterRequest);

    [Given(@"the same filter is not already present in the student's favorites")]
    public async Task GivenTheSameFilterIsNotAlreadyPresentInTheStudentsFavorites()
    {
        using var connection = new SqlConnection(DatabaseConnectionString);
        await connection.ExecuteAsync("delete catalog.FavoriteFilters where StudentId = @StudentId and Filter = @NewFilter", getAddedFavoriteFilterQueryParameters);
    }

    [Given(@"the student has not reached the maximum number of allowed favorite filters \(he has '([^']*)'\)")]
    public async Task GivenTheStudentHasNotReachedTheMaximumNumberOfAllowedFavoriteFiltersHeHas(int numberOfAlreadyAddedFavoriteFilters) => await AddFavoriteFilters(numberOfAlreadyAddedFavoriteFilters);

    [When(@"the student tries add a new filter")]
    public async Task WhenTheStudentTriesAddANewFilter() => await httpClient.PostAsJsonAsync(AddFavoriteFilterEndpoint, addNewFavoriteFilterRequest);

    [Then(@"the new filter should not be added to the student's favorites")]
    public async Task ThenTheNewFilterShouldNotBeAddedToTheStudentsFavorites()
    {
        using var connection = new SqlConnection(DatabaseConnectionString);
        var favoriteFilters = await connection.QueryAsync<AddFavoriteFilterResponse>(GetAddedFavoriteFilterQuery, getAddedFavoriteFilterQueryParameters);

        favoriteFilters.Should().NotContain(favoriteFilter => favoriteFilter.StudentId!.Value.ToString().ToUpper() == StudentId && favoriteFilter.Filter == NewFilter);
    }

    [Then(@"the new filter should not be added for a second time to the student's favorites")]
    public async Task ThenTheNewFilterShouldNotBeAddedASecondTimeToTheStudentsFavorites()
    {
        using var connection = new SqlConnection(DatabaseConnectionString);
        var favoriteFilters = await connection.QueryAsync<AddFavoriteFilterResponse>(GetAddedFavoriteFilterQuery, getAddedFavoriteFilterQueryParameters);

        favoriteFilters.Should().ContainSingle(favoriteFilter => favoriteFilter.StudentId!.Value.ToString().ToUpper() == StudentId && favoriteFilter.Filter == NewFilter);
    }

    [Then(@"the number of the students favorite filters should remain at the maximum")]
    public async Task ThenTheNumberOfTheStudentsFavoriteFiltersShouldRemainAtTheMaximum()
    {
        using var connection = new SqlConnection(DatabaseConnectionString);
        var favoriteFilters = await connection.QueryAsync<AddFavoriteFilterResponse>(GetAddedFavoriteFilterQuery, getAddedFavoriteFilterQueryParameters);

        favoriteFilters.Should().HaveCount(MaximumNumberOfFiltersForAStudent);
    }

    [Then(@"the new filter should be added to the student's favorites")]
    public async Task ThenTheNewFilterShouldBeAddedToTheStudentsFavorites()
    {
        using var connection = new SqlConnection(DatabaseConnectionString);
        var favoriteFilters = await connection.QueryAsync<AddFavoriteFilterResponse>(GetAddedFavoriteFilterQuery, getAddedFavoriteFilterQueryParameters);

        favoriteFilters.Should().HaveCount(1);
        favoriteFilters.Single().StudentId!.Value.ToString().ToUpper().Should().Be(StudentId);
        favoriteFilters.Single().Filter.Should().Be(NewFilter);
    }

    private async Task AddFavoriteFilters(int numberOfFavoriteFiltersToAdd)
    {
        var addFavoriteFilterRequestTasks = new List<Task<HttpResponseMessage>>();
        for (var addFavoriteFilterRequestNumber = 1; addFavoriteFilterRequestNumber <= numberOfFavoriteFiltersToAdd; addFavoriteFilterRequestNumber++)
        {
            var addFavoriteFilterRequest = new AddFavoriteFilterRequest
            {
                StudentId = StudentId,
                Filter = $"&favoriteFilterNumber={addFavoriteFilterRequestNumber}"
            };

            var addFavoriteFilterRequestTask = httpClient.PostAsJsonAsync(AddFavoriteFilterEndpoint, addFavoriteFilterRequest);

            addFavoriteFilterRequestTasks.Add(addFavoriteFilterRequestTask);
        }

        await Task.WhenAll(addFavoriteFilterRequestTasks);
    }
}
