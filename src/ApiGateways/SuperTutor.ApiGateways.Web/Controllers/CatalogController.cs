using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Web.Models.Catalog.GetTutorProfilesByFilter;
using SuperTutor.ApiGateways.Web.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using System.Text.Json;

namespace SuperTutor.ApiGateways.Web.Controllers;

public class CatalogController : ApiController
{
    private static readonly HttpClient httpClient = new();

    private readonly string CatalogApiUrl;

    public CatalogController(IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions) => CatalogApiUrl = apiUrlsOptions.Value.Catalog;

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<GetTutorProfilesByFilterResponse>> GetTutorProfilesByFilter([FromJsonQuery] GetTutorProfilesByFilterRequest query, CancellationToken cancellationToken)
    {
        var queryString = $"{CatalogApiUrl}/TutorProfiles/GetByFilter?query={JsonSerializer.Serialize(query)}";

        var response = await httpClient.GetFromJsonAsync<GetTutorProfilesByFilterResponse>(queryString, cancellationToken: cancellationToken);
        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }
}
