using System.Diagnostics.CodeAnalysis;

namespace RestfulApiSample.Models;

public record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "Um valor nulo foi fornecido.");
}

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException();

            case false when error == Error.None:
                throw new InvalidOperationException();

            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result<T> Success<T>(T value) => new(value, true, Error.None);
    public static Result<T> Failure<T>(Error error) => new(default, false, error);

    public static Result<T> Create<T>(T? value) =>
        value is not null ? Success(value) : Failure<T>(Error.NullValue);
}

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
        => _value = value;

    [NotNull]
    public T Value => _value! ?? throw new InvalidOperationException("Result has no value");

    public static implicit operator Result<T>(T? value) => Create(value);
}


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

        Result problemResult = Result.Failure(result.Errors);

        if (withEnglishMessage)
            problemResult.WithMessage("Failure");

        return new ObjectResult(problemResult) { StatusCode = (int)result.Status };
    }

 public static ProblemDetails ToProblemDetails(this Result result)
     => new()
     {
         Status = (int)result.Status,
         Type = ((ErrorType)result.Status).ToString(),
         Title = ((ErrorType)result.Status).GetTitle(),
         Detail = result.Message,
         Extensions =
         {
             ["Errors"] = result.Errors
         }
     };
