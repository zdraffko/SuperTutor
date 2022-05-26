using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToActionResult(this Result result)
    {
        if (result.IsFailed)
        {
            return new BadRequestObjectResult(result.Errors.FirstOrDefault()?.Message);
        }

        return new OkResult();
    }

    public static ActionResult<TPayload> ToActionResult<TPayload>(this Result<TPayload> result)
    {
        if (result.IsFailed)
        {
            return new BadRequestObjectResult(result.Errors.FirstOrDefault()?.Message);
        }

        return result.Value;
    }
}
