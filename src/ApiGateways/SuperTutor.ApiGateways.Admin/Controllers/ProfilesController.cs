using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Admin.Models.Profiles.ApproveTutorProfile;
using SuperTutor.ApiGateways.Admin.Models.Profiles.GetAllTutorProfilesForReview;
using SuperTutor.ApiGateways.Admin.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using System.Security.Claims;

namespace SuperTutor.ApiGateways.Admin.Controllers;

public class ProfilesController : ApiController
{
    private static readonly HttpClient httpClient = new();
    private readonly string ProfilesApiUrl;
    private readonly IHttpContextAccessor httpContextAccessor;

    public ProfilesController(IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions)
    {
        this.httpContextAccessor = httpContextAccessor;
        ProfilesApiUrl = apiUrlsOptions.Value.Profiles;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<GetAllTutorProfilesForReviewResponse>> GetAllTutorProfilesForReview(CancellationToken cancellationToken)
    {
        var queryString = $"{ProfilesApiUrl}/TutorProfiles/GetAllForReview?query={new { }}";

        var response = await httpClient.GetFromJsonAsync<GetAllTutorProfilesForReviewResponse>(queryString, cancellationToken: cancellationToken);

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> ApproveTutorProfile(ApproveTutorProfileRequest request, CancellationToken cancellationToken)
    {
        var adminId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (adminId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var profilesRequest = new
        {
            AdminId = adminId,
            request.TutorProfileId
        };

        var response = await httpClient.PostAsJsonAsync($"{ProfilesApiUrl}/TutorProfiles/Approve", profilesRequest, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }
}
