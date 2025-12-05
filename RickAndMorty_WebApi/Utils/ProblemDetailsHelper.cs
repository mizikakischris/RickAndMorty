using FluentResults;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Common.Errors;

namespace RickAndMorty.WebApi.Utils;

public static class ProblemDetailsHelper
{
    public static ProblemDetails CreateProblemDetailsForStatusCode(HttpContext context, IEnumerable<IError> errors, int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => Create400ApiProblemDetails(context, errors),
            StatusCodes.Status404NotFound => Create404ApiProblemDetails(context, errors),
            StatusCodes.Status500InternalServerError => Create500ApiProblemDetails(context, errors),
            _ => Create500ApiProblemDetails(context, errors)
        };
    }

    private static ProblemDetails Create400ApiProblemDetails(HttpContext context, IEnumerable<IError> errors)
        => CreateProblemDetails(context, errors, StatusCodes.Status400BadRequest);

    private static ProblemDetails Create404ApiProblemDetails(HttpContext context, IEnumerable<IError> errors)
        => CreateProblemDetails(context, errors, StatusCodes.Status404NotFound);

    private static ProblemDetails Create500ApiProblemDetails(HttpContext context, IEnumerable<IError> errors)
        => CreateProblemDetails(context, errors, StatusCodes.Status500InternalServerError);

    private static ProblemDetails CreateProblemDetails(
       HttpContext httpContext,
       IEnumerable<IError> errors,
       int statusCode)
    {
        var firstError = errors.FirstOrDefault();
        var errorBase = firstError as ErrorBase;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Instance = httpContext.Request.Path,
            Title = errorBase?.Title,
            Detail = firstError?.Message ?? "An unknown error occurred.",
            Type = null
        };

        return problemDetails;
    }
}
