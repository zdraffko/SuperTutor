using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Web.Models.Profiles.CreateTutorProfile;
using SuperTutor.ApiGateways.Web.Models.Profiles.DeleteTutorProfile;
using SuperTutor.ApiGateways.Web.Models.Profiles.GetAllTutorProfilesForTutor;
using SuperTutor.ApiGateways.Web.Models.Profiles.SubmitTutorProfileForReview;
using SuperTutor.ApiGateways.Web.Models.Profiles.UpdateTutorProfileAbout;
using SuperTutor.ApiGateways.Web.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using System.Security.Claims;
using System.Text.Json;

namespace SuperTutor.ApiGateways.Web.Controllers;

public class ProfilesController : ApiController
{
    private static readonly HttpClient httpClient = new();

    private readonly string ProfilesApiUrl;
    private readonly IHttpContextAccessor httpContextAccessor;

    public ProfilesController(IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
    {
        ProfilesApiUrl = apiUrlsOptions.Value.Profiles;
        this.httpContextAccessor = httpContextAccessor;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateTutorProfile(CreateTutorProfileRequest request, CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var profilesRequest = new
        {
            TutorId = tutorId,
            request.TutoringSubject,
            request.TutoringGrades,
            request.RateForOneHour,
            request.About
        };

        var response = await httpClient.PostAsJsonAsync($"{ProfilesApiUrl}/TutorProfiles/Create", profilesRequest, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var responsePayload = await response.Content.ReadFromJsonAsync<CreateTutorProfileResponse>(cancellationToken: cancellationToken);

            return Ok(responsePayload);
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<GetAllTutorProfilesForTutorResponse>> GetAllTutorProfilesForTutor(CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var profilesRequest = new
        {
            TutorId = tutorId
        };

        var queryString = $"{ProfilesApiUrl}/TutorProfiles/GetAllForTutor?query={JsonSerializer.Serialize(profilesRequest)}";

        var response = await httpClient.GetFromJsonAsync<GetAllTutorProfilesForTutorResponse>(queryString, cancellationToken: cancellationToken);

        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UpdateTutorProfileAbout(UpdateTutorProfileAboutRequest request, CancellationToken cancellationToken)
    {
        var profilesRequest = new
        {
            request.TutorProfileId,
            request.NewAbout
        };

        var response = await httpClient.PostAsJsonAsync($"{ProfilesApiUrl}/TutorProfiles/UpdateAbout", profilesRequest, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> SubmitTutorProfileForReview(SubmitTutorProfileForReviewRequest request, CancellationToken cancellationToken)
    {
        var profilesRequest = new
        {
            request.TutorProfileId
        };

        var response = await httpClient.PostAsJsonAsync($"{ProfilesApiUrl}/TutorProfiles/SubmitForReview", profilesRequest, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> DeleteTutorProfile(DeleteTutorProfileRequest request, CancellationToken cancellationToken)
    {
        var profilesRequest = new
        {
            request.TutorProfileId
        };

        var response = await httpClient.PostAsJsonAsync($"{ProfilesApiUrl}/TutorProfiles/Delete", profilesRequest, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }
}
