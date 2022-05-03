using Dapper;
using FluentAssertions;
using SuperTutor.Contexts.Catalog.Tests.Acceptance.Shared;
using SuperTutor.Contexts.Catalog.Tests.Acceptance.TutorProfiles.Queries.GetByFilter.Models;
using SuperTutor.Contexts.Catalog.Tests.Acceptance.TutorProfiles.Shared;
using System.Data.SqlClient;
using System.Net.Http.Json;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace SuperTutor.Contexts.Catalog.Tests.Acceptance.TutorProfiles.Queries.GetByFilter;

[Binding]
public class GetTutorProfilesByFilterStepDefinitions
{
    private readonly HttpClient httpClient;
    private readonly GetTutorProfilesByFilterFilter filter;
    private readonly Dictionary<string, int> tutoringSubjectsMapping;
    private readonly Dictionary<int, string> tutoringGradesMapping;
    private GetTutorProfilesByFilterResponse? getTutorProfilesByFilterResponse;

    public GetTutorProfilesByFilterStepDefinitions(HttpClient httpClient)
    {
        this.httpClient = httpClient;

        tutoringSubjectsMapping = new Dictionary<string, int>
        {
            { "Math", 0 },
            { "Bulgarian", 1 },
            { "Literature", 2 }
        };

        tutoringGradesMapping = new Dictionary<int, string>
        {
            { 9, "Ninth"},
            { 10, "Tenth" },
            { 11, "Eleventh" }
        };

        filter = new GetTutorProfilesByFilterFilter
        {
            TutoringSubjects = new List<int> { 0, 1, 2 },
            TutoringGrades = new List<int> { 8, 9, 10, 11 },
            MinRateForOneHour = 1,
            MaxRateForOneHour = 1000
        };

        getTutorProfilesByFilterResponse = new GetTutorProfilesByFilterResponse();
    }

    [Given(@"There are tutor profiles in the catalog")]
    public async Task ThereAreTutorProfilesInTheCatalog()
    {
        using var connection = new SqlConnection(Constants.DatabaseConnectionString);

        await connection.ExecuteAsync("insert into catalog.TutorProfiles values (@Id, @About, @TutoringSubject_Value, @TutoringSubject_Name, @RateForOneHour, @IsActive)",
            new { Id = "FEF080E6-B79B-43FC-8E6E-66F7931DA89B", About = "about text", TutoringSubject_Value = 2, TutoringSubject_Name = "Literature", RateForOneHour = 20m, IsActive = 1 });
        await connection.ExecuteAsync("insert into catalog.TutorProfileTutoringGrades (TutorProfileId, Value, Name) values (@TutorProfileId, @Value, @Name)",
             new { TutorProfileId = "FEF080E6-B79B-43FC-8E6E-66F7931DA89B", Value = 10, Name = "Tenth" });
        await connection.ExecuteAsync("insert into catalog.TutorProfileTutoringGrades (TutorProfileId, Value, Name)  values (@TutorProfileId, @Value, @Name)",
             new { TutorProfileId = "FEF080E6-B79B-43FC-8E6E-66F7931DA89B", Value = 11, Name = "Eleventh" });

        await connection.ExecuteAsync("insert into catalog.TutorProfiles values (@Id, @About, @TutoringSubject_Value, @TutoringSubject_Name, @RateForOneHour, @IsActive)",
            new { Id = "891A26A0-9340-4065-BB84-842B1824367B", About = "about text 2", TutoringSubject_Value = 1, TutoringSubject_Name = "Bulgarian", RateForOneHour = 2m, IsActive = 1 });
        await connection.ExecuteAsync("insert into catalog.TutorProfileTutoringGrades (TutorProfileId, Value, Name)  values (@TutorProfileId, @Value, @Name)",
             new { TutorProfileId = "891A26A0-9340-4065-BB84-842B1824367B", Value = 9, Name = "Ninth" });

        await connection.ExecuteAsync("insert into catalog.TutorProfiles values (@Id, @About, @TutoringSubject_Value, @TutoringSubject_Name, @RateForOneHour, @IsActive)",
            new { Id = "38B96011-D086-4E15-07BC-08DA260B684A", About = "about text 3", TutoringSubject_Value = 0, TutoringSubject_Name = "Math", RateForOneHour = 120m, IsActive = 1 });
        await connection.ExecuteAsync("insert into catalog.TutorProfileTutoringGrades (TutorProfileId, Value, Name) values (@TutorProfileId, @Value, @Name)",
          new { TutorProfileId = "38B96011-D086-4E15-07BC-08DA260B684A", Value = 8, Name = "Eight" });
    }

    [Given(@"Alex selects '([^']*)' as the maximum rate for one hour for the tutor profiles that he is searching for")]
    public void GivenAlexSelectsAsTheMaximumRateForOneHourForTheTutorProfilesThatHeIsSearchingFor(decimal maxRateForOneHour)
        => filter.MaxRateForOneHour = maxRateForOneHour;

    [Given(@"Alex selects '([^']*)' as the tutoring subjects for the tutor profiles that he is searching for")]
    public void GivenAlexSelectsAsTheTutoringSubjectsForTheTutorProfilesThatHeIsSearchingFor(string input)
    {
        var tutoringSubjects = input.Split(',').Select(rawSubjectValue => int.Parse(rawSubjectValue));
        filter.TutoringSubjects = tutoringSubjects;
    }

    [Given(@"Alex selects '([^']*)' as the tutoring grades for the tutor profiles that he is searching for")]
    public void GivenAlexSelectsAsTheTutoringGradesForTheTutorProfilesThatHeIsSearchingFor(string input)
    {
        var tutoringGrades = input.Split(',').Select(rawSubjectValue => int.Parse(rawSubjectValue));
        filter.TutoringGrades = tutoringGrades;
    }

    [Given(@"Alex selects '([^']*)' as the minimum rate for one hour for the tutor profiles that he is searching for")]
    public void GivenAlexSelectsAsTheMinimumRateForOneHourForTheTutorProfilesThatHeIsSearchingFor(decimal minRateForOneHour)
        => filter.MinRateForOneHour = minRateForOneHour;

    [Given(@"there are inactive tutor profiles")]
    public async Task GivenThereAreInactiveTutorProfiles()
    {
        using var connection = new SqlConnection(Constants.DatabaseConnectionString);

        await connection.ExecuteAsync("insert into catalog.TutorProfiles values (@Id, @About, @TutoringSubject_Value, @TutoringSubject_Name, @RateForOneHour, @IsActive)",
            new { Id = "8F25F237-E079-44A1-B063-6E2F03A1EE3B", About = "inactive", TutoringSubject_Value = 2, TutoringSubject_Name = "Literature", RateForOneHour = 20m, IsActive = 0 });
        await connection.ExecuteAsync("insert into catalog.TutorProfileTutoringGrades (TutorProfileId, Value, Name) values (@TutorProfileId, @Value, @Name)",
            new { TutorProfileId = "8F25F237-E079-44A1-B063-6E2F03A1EE3B", Value = 8, Name = "Eight" });

        await connection.ExecuteAsync("insert into catalog.TutorProfiles values (@Id, @About, @TutoringSubject_Value, @TutoringSubject_Name, @RateForOneHour, @IsActive)",
            new { Id = "1EBC355F-87BD-4F4F-07BD-08DA260B684A", About = "inactive", TutoringSubject_Value = 0, TutoringSubject_Name = "Math", RateForOneHour = 50m, IsActive = 0 });
        await connection.ExecuteAsync("insert into catalog.TutorProfileTutoringGrades (TutorProfileId, Value, Name) values (@TutorProfileId, @Value, @Name)",
            new { TutorProfileId = "1EBC355F-87BD-4F4F-07BD-08DA260B684A", Value = 8, Name = "Eight" });
    }

    [When(@"Alex searches for tutor profiles")]
    public async Task WhenAlexSearchesForTutorProfiles()
        => getTutorProfilesByFilterResponse = await httpClient.GetFromJsonAsync<GetTutorProfilesByFilterResponse>($"{TutorProfileConstants.GetTutorProfilesByFilter}?query={JsonSerializer.Serialize(filter)}");

    [Then(@"only the profiles that have one of the specified tutoring subjects should be returned")]
    public void ThenOnlyTheProfilesThatHaveOneOfTheSpecifiedTutoringSubjectsShouldBeReturned()
        => getTutorProfilesByFilterResponse?.TutorProfiles.Should().AllSatisfy(tutorProfile => filter.TutoringSubjects.Should().Contain(tutoringSubjectsMapping[tutorProfile.TutoringSubject]));

    [Then(@"only the profiles that have one of the specified tutoring grades should be returned")]
    public void ThenOnlyTheProfilesThatHaveOneOfTheSpecifiedTutoringGradesShouldBeReturned()
        => getTutorProfilesByFilterResponse?.TutorProfiles.Should().AllSatisfy(tutorProfile => tutorProfile.TutoringGrades.Should().IntersectWith(filter.TutoringGrades.Select(tutoringGradeKey => tutoringGradesMapping[tutoringGradeKey])));

    [Then(@"only the profiles that have a rate for one hour that is above or equal to the one specified should be returned")]
    public void ThenOnlyTheProfilesThatHaveARateForOneHourThatIsAboveTheOneSpecifiedShouldBeReturned()
        => getTutorProfilesByFilterResponse?.TutorProfiles.Should().AllSatisfy(tutorProfile => tutorProfile.RateForOneHour.Should().BeGreaterThanOrEqualTo(filter.MinRateForOneHour));

    [Then(@"only the profiles that have a rate for one hour that is below or equal to the one specified should be returned")]
    public void ThenOnlyTheProfilesThatHaveARateForOneHourThatIsBelowTheOneSpecifiedShouldBeReturned()
        => getTutorProfilesByFilterResponse?.TutorProfiles.Should().AllSatisfy(tutorProfile => tutorProfile.RateForOneHour.Should().BeLessThanOrEqualTo(filter.MaxRateForOneHour));

    [Then(@"only the active tutor profiles should be returned")]
    public void ThenOnlyTheActiveTutorProfilesShouldBeReturned()
        => getTutorProfilesByFilterResponse?.TutorProfiles.Should().AllSatisfy(tutorProfile => tutorProfile.About.Should().NotBeEmpty().And.NotBe("inactive"));
}
