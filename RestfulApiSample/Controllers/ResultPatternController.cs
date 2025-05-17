using Microsoft.AspNetCore.Mvc;
using RestfulApiSample.Models;

namespace RestfulApiSample.Controllers;

public class ResultPatternController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> RegisterUser(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure(Error.InvalidEmail);

        if (string.IsNullOrWhiteSpace(password))
            return Result.Failure(Error.InvalidPassword);

        
        return Result.Success();
    }
}
