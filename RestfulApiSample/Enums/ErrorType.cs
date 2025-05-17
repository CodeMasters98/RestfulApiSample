using RestfulApiSample.Attributes;
using System.ComponentModel;
using System.Net;

namespace RestfulApiSample.Enums;

public enum ErrorType
{
    [Value<HttpStatusCode>(HttpStatusCode.BadRequest)]
    [Description("Bad Request")]
    VALIDATION = 400,

    [Value<HttpStatusCode>(HttpStatusCode.OK)]
    [Description("Business Failure")]
    FAILURE = 200,

    [Value<HttpStatusCode>(HttpStatusCode.InternalServerError)]
    [Description("Internal Server Error")]
    EXCEPTION = 500,

    [Value<HttpStatusCode>(HttpStatusCode.NotFound)]
    [Description("Not Found")]
    NOT_FOUND = 404,

    [Value<HttpStatusCode>(HttpStatusCode.Unauthorized)]
    [Description("Unauthorized Access")]
    UNAUTHORIZED = 401,
}
