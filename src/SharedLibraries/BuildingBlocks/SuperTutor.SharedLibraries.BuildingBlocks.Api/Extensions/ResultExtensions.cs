using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Api.Extensions;

internal static class ResultExtensions
{
    internal static ActionResult ToActionResult(this Result result)
    {
        if (result.IsFailed)
        {
            return new BadRequestObjectResult(result.Errors.Select(error => error.Message));
        }

        return new OkResult();
    }

    internal static ActionResult<TPayload> ToActionResult<TPayload>(this Result<TPayload> result)
    {
        if (result.IsFailed)
        {
            return new BadRequestObjectResult(result.Errors.Select(error => error.Message));
        }

        return result.Value;
    }
}
