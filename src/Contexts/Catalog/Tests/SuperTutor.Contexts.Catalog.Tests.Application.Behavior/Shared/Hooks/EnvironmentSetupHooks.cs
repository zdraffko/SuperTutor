using BoDi;
using Dapper;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using System.Data.SqlClient;
using System.Net;
using TechTalk.SpecFlow;

namespace SuperTutor.Contexts.Catalog.Tests.Acceptance.Shared.Hooks;

[Binding]
public class EnvironmentSetupHooks
{
    private static ICompositeService compositeService;
    private readonly IObjectContainer objectContainer;

    public EnvironmentSetupHooks(IObjectContainer objectContainer) => this.objectContainer = objectContainer;

    [BeforeTestRun]
    public static void DockerComposeUp()
    {
        var apiUrl = "http://localhost:5002/health";
        var dockerComposePath = GetDockerComposePath("docker-compose.Catalog.Tests.yml");

        compositeService = new Builder()
            .UseContainer()
            .UseCompose().FromFile(dockerComposePath).ServiceName("supertutor-catalog-test").ForceBuild()
            .RemoveOrphans()
            .WaitForHttp("supertutor-catalog-test", apiUrl, method: HttpMethod.Get, continuation: (response, _) => response.Code == HttpStatusCode.OK ? 0 : 1000)
            .Build()
            .Start();
    }

    [AfterTestRun]
    public static void DockerComposeDown()
    {
        compositeService.Stop();
        compositeService.Dispose();
    }

    [BeforeScenario]
    public void RegisterHttpClient()
    {
        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5002")
        };

        objectContainer.RegisterInstanceAs(httpClient);
    }

    [BeforeScenario]
    public async Task CleanDatabase()
    {
        using var connection = new SqlConnection("Server=localhost;Database=SuperTutorCatalogTest;User Id=sa;Password=testPass123;MultipleActiveResultSets=true");

        await connection.ExecuteAsync("EXEC sys.sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
        await connection.ExecuteAsync("EXEC sys.sp_msforeachtable 'DELETE FROM ?'");
        await connection.ExecuteAsync("EXEC sys.sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");
    }

    private static string GetDockerComposePath(string dockerComposeFileName)
    {
        var directory = Directory.GetCurrentDirectory();
        while (!Directory.EnumerateFiles(directory, "*.yml").Any(fileName => fileName.EndsWith(dockerComposeFileName)))
        {
            directory = directory[..directory.LastIndexOf(Path.DirectorySeparatorChar)];
        }

        return Path.Combine(directory, dockerComposeFileName);
    }
}
