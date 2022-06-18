using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Admin.Models.Profiles.GetAllTutorProfilesForReview;
using SuperTutor.ApiGateways.Admin.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;

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
}
