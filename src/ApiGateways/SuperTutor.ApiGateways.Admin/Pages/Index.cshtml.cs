using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Admin.Models.Profiles;
using SuperTutor.ApiGateways.Admin.Options;
using System.Security.Claims;
using System.Text.Json;

namespace SuperTutor.ApiGateways.Admin.Pages;

public class IndexModel : PageModel
{
    private static readonly HttpClient httpClient = new();
    private readonly string ProfilesApiUrl;
    private readonly IHttpContextAccessor httpContextAccessor;

    public IndexModel(IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
    {
        ProfilesApiUrl = apiUrlsOptions.Value.Profiles;
        TutorProfiles = new List<TutorProfile>();
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<ActionResult> OnGet()
    {
        var authenticationResult = await httpContextAccessor.HttpContext.AuthenticateAsync("Cookies");
        if (!authenticationResult.Succeeded)
        {
            return RedirectToPage("Register");
        }

        var cancellationToken = new CancellationTokenSource().Token;
        var queryString = $"{ProfilesApiUrl}/TutorProfiles/GetAllForReview?query={JsonSerializer.Serialize(new { })}";

        var response = await httpClient.GetFromJsonAsync<GetAllTutorProfilesForReviewResponse>(queryString, cancellationToken: cancellationToken);
        TutorProfiles = response?.TutorProfiles ?? Enumerable.Empty<TutorProfile>();

        return Page();
    }

    public IEnumerable<TutorProfile> TutorProfiles { get; set; }

    public async Task<ActionResult> OnPostApproveTutorProfile(string tutorProfileId)
    {
        var cancellationToken = new CancellationTokenSource().Token;

        var authenticationResult = await httpContextAccessor.HttpContext.AuthenticateAsync("Cookies");
        var claim = authenticationResult.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        var adminId = claim.Value;
        var profilesRequest = new
        {
            TutorProfileId = tutorProfileId,
            AdminId = adminId
        };

        await httpClient.PostAsJsonAsync($"{ProfilesApiUrl}/TutorProfiles/Approve", profilesRequest, cancellationToken: cancellationToken);

        return RedirectToPage("Index");
    }
}
