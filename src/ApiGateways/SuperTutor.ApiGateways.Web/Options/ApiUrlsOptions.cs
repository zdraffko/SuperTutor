namespace SuperTutor.ApiGateways.Web.Options;

public class ApiUrlsOptions
{
    public const string SectionName = "ApiUrls";

    public string Identity { get; set; } = string.Empty;

    public string Payments { get; set; } = string.Empty;

    public string Profiles { get; set; } = string.Empty;

    public string Catalog { get; set; } = string.Empty;

    public string Schedule { get; set; } = string.Empty;

    public string Classrooms { get; set; } = string.Empty;
}
