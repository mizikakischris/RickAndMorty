using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Common.Errors;

namespace RickAndMorty.Common.Utils;

public static class ActionResultHelper
{
    public static ActionResult<TDomainModel> ToActionResult<TDomainModel>(
        this Result<TDomainModel> result,
        HttpContext context) where TDomainModel : notnull
    {
        return result.IsSuccess ? new OkObjectResult(result.Value) : CreateErrorResult(result, context);
    }

    private static ObjectResult CreateErrorResult(
    ResultBase result,
    HttpContext context)
    {
        var statusCode = result.Errors
                               .OfType<ErrorBase>()
                               .FirstOrDefault()?.StatusCode ?? StatusCodes.Status500InternalServerError;

        var problemDetails = ProblemDetailsHelper.CreateProblemDetailsForStatusCode(context, result.Errors, statusCode);

        return new ObjectResult(problemDetails) { StatusCode = statusCode };
    }
}
