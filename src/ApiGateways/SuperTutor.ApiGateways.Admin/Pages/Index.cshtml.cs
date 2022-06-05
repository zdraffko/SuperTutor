using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Admin.Models.Profiles;
using SuperTutor.ApiGateways.Admin.Options;
using System.Text.Json;

namespace SuperTutor.ApiGateways.Admin.Pages;
public class IndexModel : PageModel
{
    private static readonly HttpClient httpClient = new();
    private readonly string ProfilesApiUrl;

    public IndexModel(IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions)
    {
        ProfilesApiUrl = apiUrlsOptions.Value.Profiles;
        TutorProfiles = new List<TutorProfile>();
    }

    public async Task OnGet()
    {
        var cancellationToken = new CancellationTokenSource().Token;
        var queryString = $"{ProfilesApiUrl}/TutorProfiles/GetAllForReview?query={JsonSerializer.Serialize(new { })}";

        var response = await httpClient.GetFromJsonAsync<GetAllTutorProfilesForReviewResponse>(queryString, cancellationToken: cancellationToken);
        TutorProfiles = response?.TutorProfiles ?? Enumerable.Empty<TutorProfile>();
    }

    public IEnumerable<TutorProfile> TutorProfiles { get; set; }
}
