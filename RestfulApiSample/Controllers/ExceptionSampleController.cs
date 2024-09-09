using Microsoft.AspNetCore.Mvc;

namespace RestfulApiSample.Controllers;

public class ExceptionSampleController : Controller
{
    //Step 1
    //Return Exceptions
    public void SendNotificationEmail_WithDirectException(string userEmail, string templateId)
    {
        if (string.IsNullOrEmpty(userEmail))
            throw new ArgumentException("user email is null or empty", nameof(userEmail));

        if (string.IsNullOrEmpty(templateId))
            throw new ArgumentException("templateId is null or empty", nameof(templateId));

        //Some Other Business
    }

    //Step 2
    //Diffrent Class
    public void SendNotificationEmail_WithExceptionClass(string userEmail, string templateId)
    {
        if (string.IsNullOrEmpty(userEmail))
            throw new UserEmailInvalid();

        if (string.IsNullOrEmpty(templateId))
            throw new TemplateInvalid();

        //Some Other Business
    }

    //Step 3
    //Direct object
    public string SendNotificationEmail_WithDirectMessage(string userEmail, string templateId)
    {
        if (string.IsNullOrEmpty(userEmail))
            return "user email is null or empty";

        if (string.IsNullOrEmpty(templateId))
            return "templateId is null or empty";

        //Some Other Business
        return string.Empty;
    }

    //Step 4
    //Direct object
    public Error_Type1 SendNotificationEmail_WithDirectError(string userEmail, string templateId)
    {
        if (string.IsNullOrEmpty(userEmail))
            return new Error_Type1(Status: 400, Message: "user email is null or empty");

        if (string.IsNullOrEmpty(templateId))
            return new Error_Type1(Status: 400, Message: "templateId is null or empty");

        //Some Other Business
        return new Error_Type1(200, string.Empty);
    }

    //Step 5
    //Direct object
    public Error_Type2 SendNotificationEmail_WithDirectError2(string userEmail, string templateId)
    {
        if (string.IsNullOrEmpty(userEmail))
            return new Error_Type2(Status: 400, Message: "user email is null or empty");

        if (string.IsNullOrEmpty(templateId))
            return new Error_Type2(Status: 400, Message: "templateId is null or empty");

        //Some Other Business
        return Error_Type2.None;
    }

    //Step 6
    //Direct object
    public Error_Type3 SendNotificationEmail_WithDirectError3(string userEmail, string templateId)
    {
        if (string.IsNullOrEmpty(userEmail))
            return MyErrors.InvalidEmail;

        if (string.IsNullOrEmpty(templateId))
            return MyErrors.InvalidTemplate;

        //Some Other Business
        return Error_Type3.None;
    }

    //Step 7
    //Result Pattern
    public Result SendNotificationEmail_Result(string userEmail, string templateId)
    {
        if (string.IsNullOrEmpty(userEmail))
            return Result.Failure(MyErrors.InvalidEmail);

        if (string.IsNullOrEmpty(templateId))
            return Result.Failure(MyErrors.InvalidTemplate);

        //Some Other Business
        return Result.Success();
    }
}


//Class For Step 2
public sealed class UserEmailInvalid : Exception
{
    public UserEmailInvalid() : base("user email is null or empty")
    {

    }
}
public sealed class TemplateInvalid : Exception
{
    public TemplateInvalid() : base("templateId is null or empty")
    {

    }
}

//For Step 4
public sealed record Error_Type1(int Status, string? Message = null);

//For Step 5
public sealed record Error_Type2(int Status, string? Message = null)
{
    public static readonly Error_Type2 None = new(200);
}

//For Step 6
public sealed record Error_Type3(int Status, string? Message = null)
{
    public static readonly Error_Type3 None = new(200);
}

public sealed class MyErrors
{
    public static readonly Error_Type3 InvalidEmail = new Error_Type3(Status: 400, Message: "user email is null or empty");
    public static readonly Error_Type3 InvalidTemplate = new Error_Type3(Status: 400, Message: "templateId is null or empty");
}

//For Step 7
public class Result
{
    private Result(bool isSuccess,Error_Type3 error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure  => !IsSuccess;
    public Error_Type3 Error { get; }

    public static Result Success() => new(true, Error_Type3.None);
    public static Result Failure(Error_Type3 error) => new(false, error);
}