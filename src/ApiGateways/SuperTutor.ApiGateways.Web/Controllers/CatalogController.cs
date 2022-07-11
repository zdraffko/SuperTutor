using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Web.Constants;
using SuperTutor.ApiGateways.Web.Models.Catalog.GetTutorAvailability;
using SuperTutor.ApiGateways.Web.Models.Catalog.GetTutorProfileById;
using SuperTutor.ApiGateways.Web.Models.Catalog.GetTutorProfilesByFilter;
using SuperTutor.ApiGateways.Web.Models.Catalog.ReserveTrialLesson;
using SuperTutor.ApiGateways.Web.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;
using System.Security.Claims;
using System.Text.Json;

namespace SuperTutor.ApiGateways.Web.Controllers;

public class CatalogController : ApiController
{
    private static readonly JsonSerializerOptions jsonSerializerOptions = new();
    private static readonly HttpClient httpClient = new();
    private readonly string CatalogApiUrl;
    private readonly string ScheduleApiUrl;
    private readonly IHttpContextAccessor httpContextAccessor;

    static CatalogController()
    {
        jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        jsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    }

    public CatalogController(IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
    {
        CatalogApiUrl = apiUrlsOptions.Value.Catalog;
        ScheduleApiUrl = apiUrlsOptions.Value.Schedule;
        this.httpContextAccessor = httpContextAccessor;
    }

    [Authorize(Roles = Roles.Student)]
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

    [Authorize(Roles = Roles.Student)]
    [HttpGet]
    public async Task<ActionResult<GetTutorProfileByIdResponse>> GetTutorProfileById([FromJsonQuery] GetTutorProfileByIdRequest query, CancellationToken cancellationToken)
    {
        var queryString = $"{CatalogApiUrl}/TutorProfiles/GetById?query={JsonSerializer.Serialize(query)}";

        var response = await httpClient.GetFromJsonAsync<GetTutorProfileByIdResponse>(queryString, cancellationToken: cancellationToken);
        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }

    [Authorize(Roles = Roles.Student)]
    [HttpGet]
    public async Task<ActionResult<GetTutorAvailabilityResponse>> GetTutorAvailability([FromJsonQuery] GetTutorAvailabilityQuery query, CancellationToken cancellationToken)
    {
        var scheduleRequest = new
        {
            query.TutorId
        };

        var queryString = $"{ScheduleApiUrl}/TimeSlots/GetAvailability?query={JsonSerializer.Serialize(scheduleRequest, options: jsonSerializerOptions)}";

        var response = await httpClient.GetFromJsonAsync<GetTutorAvailabilityResponse>(queryString, options: jsonSerializerOptions, cancellationToken: cancellationToken);
        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }

    [Authorize(Roles = Roles.Student)]
    [HttpPost]
    public async Task<ActionResult> ReserveTrialLesson(ReserveTrialLessonRequest request, CancellationToken cancellationToken)
    {
        var studentId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (studentId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var scheduleRequest = new
        {
            StudentId = studentId,
            request.TutorId,
            request.Date,
            request.StartTime,
            request.Subject,
            request.Grade
        };

        var response = await httpClient.PostAsJsonAsync($"{ScheduleApiUrl}/Lessons/ReserveTrialLesson", scheduleRequest, options: jsonSerializerOptions, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var responsePayload = await response.Content.ReadFromJsonAsync<ReserveTrialLessonResponse>(cancellationToken: cancellationToken);

            return Ok(responsePayload);
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }
}
