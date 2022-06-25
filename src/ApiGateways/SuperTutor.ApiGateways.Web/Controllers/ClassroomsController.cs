using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Web.Models.Classrooms.GetActiveClassroomForStudent;
using SuperTutor.ApiGateways.Web.Models.Classrooms.GetActiveClassroomForTutor;
using SuperTutor.ApiGateways.Web.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Api.Controllers;
using System.Security.Claims;
using System.Text.Json;

namespace SuperTutor.ApiGateways.Web.Controllers;

public class ClassroomsController : ApiController
{
    private static readonly HttpClient httpClient = new();
    private readonly string ClassroomsApiUrl;
    private readonly IHttpContextAccessor httpContextAccessor;

    public ClassroomsController(IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions)
    {
        this.httpContextAccessor = httpContextAccessor;
        ClassroomsApiUrl = apiUrlsOptions.Value.Classrooms;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<GetActiveClassroomForTutorResponse>> GetActiveClassroomForTutor(CancellationToken cancellationToken)
    {
        var tutorId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tutorId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var query = new
        {
            TutorId = tutorId
        };

        var queryString = $"{ClassroomsApiUrl}/classrooms/GetActiveForTutor?query={JsonSerializer.Serialize(query)}";

        var response = await httpClient.GetFromJsonAsync<GetActiveClassroomForTutorResponse>(queryString, cancellationToken: cancellationToken);

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<GetActiveClassroomForStudentResponse>> GetActiveClassroomForStudent(CancellationToken cancellationToken)
    {
        var studentId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (studentId is null)
        {
            return BadRequest("Възнокна неочаквана грешка");
        }

        var query = new
        {
            StudentId = studentId
        };

        var queryString = $"{ClassroomsApiUrl}/classrooms/GetActiveForStudent?query={JsonSerializer.Serialize(query)}";

        var response = await httpClient.GetFromJsonAsync<GetActiveClassroomForStudentResponse>(queryString, cancellationToken: cancellationToken);

        return Ok(response);
    }
}
