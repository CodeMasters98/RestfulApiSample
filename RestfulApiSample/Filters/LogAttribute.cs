using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Web.Http.Controllers;

namespace RestfulApiSample.Filters;

public class LogAttribute : Attribute, IActionFilter
{
    public LogAttribute()
    {

    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine(string.Format("Action Method {0} executing at {1}", context.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString()), "Web API Logs");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine(string.Format("Action Method {0} executed at {1}", context.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString()), "Web API Logs");
    }

    public bool AllowMultiple
    {
        get { return true; }
    }
}