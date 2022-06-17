using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Admin.Options;
using SuperTutor.ApiGateways.Web.Models.Identity.GetIdentityInfo;
using SuperTutor.ApiGateways.Web.Models.Identity.Login;
using SuperTutor.ApiGateways.Web.Models.Identity.Register;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using System.Security.Claims;
using System.Text.Json;

namespace SuperTutor.ApiGateways.Admin.Controllers;

public class IdentityController : ApiController
{
    private static readonly HttpClient httpClient = new();
    private readonly string IdentityApiUrl;
    private readonly IHttpContextAccessor httpContextAccessor;

    public IdentityController(IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions)
    {
        this.httpContextAccessor = httpContextAccessor;
        IdentityApiUrl = apiUrlsOptions.Value.Identity;
    }

    [HttpPost]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync($"{IdentityApiUrl}/users/login", request, cancellationToken: cancellationToken);
        var rawResponseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = JsonSerializer.Deserialize<LoginResponse>(rawResponseContent);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var registerEndpoint = "/users/RegisterAdmin";

        var identityRegisterRequest = new
        {
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName
        };

        var response = await httpClient.PostAsJsonAsync($"{IdentityApiUrl}{registerEndpoint}", identityRegisterRequest, cancellationToken: cancellationToken);
        var rawResponseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = JsonSerializer.Deserialize<RegisterResponse>(rawResponseContent);

        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<GetIdentityInfoResponse>> GetIdentityInfo(CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var query = new GetIdentityInfoRequest(userId);
        var queryString = $"{IdentityApiUrl}/users/GetIdentityInfo?query={JsonSerializer.Serialize(query)}";

        var response = await httpClient.GetFromJsonAsync<GetIdentityInfoResponse>(queryString, cancellationToken: cancellationToken);
        if (response is not null)
        {
            response.UserId = userId;
        }

        return Ok(response);
    }
}
