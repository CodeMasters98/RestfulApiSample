using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using RestfulApiSample.Enums;
using RestfulApiSample.Models;
using System.Net;

namespace RestfulApiSample.Extentions;

public static class ResultExtension
{
    public static ObjectResult ToObjectResult(this Result result, bool withEnglishMessage)
    {
        if (result.Status is not HttpStatusCode.OK)
            return new ObjectResult(result.ToProblemDetails()) { StatusCode = (int)result.Status };

        if (result.IsSuccess)
        {
            if (withEnglishMessage)
                result.WithMessage("Success");

            return new OkObjectResult(result);
        }

        Result problemResult = Result.Failure(result.Error);

        if (withEnglishMessage)
            problemResult.WithMessage("Failure");

        return new ObjectResult(problemResult) { StatusCode = (int)result.Status };
    }

    public static ProblemDetails ToProblemDetails(this Result result)
        => new()
        {
            Status = (int)result.Status,
            Type = ((ErrorType)result.Status).ToString(),
            Title = ((ErrorType)result.Status).GetDisplayName(),
            Detail = result.Message,
            Extensions =
            {
             ["Errors"] = result.Error
            }
        };
}
