using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Web.Models.Schedule.AddTutorAvailability;
using SuperTutor.ApiGateways.Web.Models.Schedule.GetTutorTimeSlotsForWeek;
using SuperTutor.ApiGateways.Web.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Attributes;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;
using System.Security.Claims;
using System.Text.Json;

namespace SuperTutor.ApiGateways.Web.Controllers;

public class ScheduleController : ApiController
{
    private static readonly HttpClient httpClient = new();
    private static readonly JsonSerializerOptions jsonSerializerOptions = new();

    private readonly string ScheduleApiUrl;
    private readonly IHttpContextAccessor httpContextAccessor;

    static ScheduleController()
    {
        jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        jsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    }

    public ScheduleController(IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
    {
        ScheduleApiUrl = apiUrlsOptions.Value.Schedule;
        this.httpContextAccessor = httpContextAccessor;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> AddTutorAvailability(AddTutorAvailabilityRequest request, CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var scheduleRequest = new
        {
            TutorId = tutorId,
            request.Date,
            request.StartTime
        };

        var response = await httpClient.PostAsJsonAsync($"{ScheduleApiUrl}/TimeSlots/AddAvailability", scheduleRequest, options: jsonSerializerOptions, cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        var responseErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        return BadRequest(responseErrorMessage);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<GetTutorTimeSlotsForWeekResponse>> GetTutorTimeSlotsForWeek([FromJsonQuery] GetTutorTimeSlotsForWeekRequest query, CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var scheduleRequest = new
        {
            TutorId = tutorId,
            query.WeekStartDate
        };

        var queryString = $"{ScheduleApiUrl}/TimeSlots/GetForWeek?query={JsonSerializer.Serialize(scheduleRequest, options: jsonSerializerOptions)}";

        var response = await httpClient.GetFromJsonAsync<GetTutorTimeSlotsForWeekResponse>(queryString, options: jsonSerializerOptions, cancellationToken: cancellationToken);
        if (response is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        return Ok(response);
    }
}
