using Microsoft.AspNetCore.Mvc;
using RestfulApiSample.Models;

namespace RestfulApiSample.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandController : ControllerBase
{

    public static List<Brand> brands = new List<Brand>()
    {
        new Brand() { Id = 1, Name = "Test1", CreateAt = DateTime.Now },
        new Brand() { Id = 2, Name = "Test2", CreateAt = DateTime.Now.AddHours(-5) },
    };

    [HttpGet]
    [Route("GetBrandByIdFromRoute/{id:int}")]
    public IActionResult GetBrandByIdFromRoute([FromRoute] int id)
    {
        var brand = brands.Where(x => x.Id == id).FirstOrDefault();
        return Ok(brand);
    }

    [HttpGet]
    [Route("GetBrandByIdFromQuery")]
    public IActionResult GetBrandByIdFromQuery([FromQuery] int id)
    {
        var brand = brands.Where(x => x.Id == id).FirstOrDefault();
        return Ok(brand);
    }

    [HttpGet]
    [Route("GetBrandByIdFromForm")]
    public IActionResult GetBrandByIdFromForm([FromForm] int id)
    {
        var brand = brands.Where(x => x.Id == id).FirstOrDefault();
        return Ok(brand);
    }

    [HttpGet]
    [Route("GetBrandByIdFromBody")]
    public IActionResult GetBrandByIdFromBody([FromBody] int id)
    {
        var brand = brands.Where(x => x.Id == id).FirstOrDefault();
        return Ok(brand);
    }

    [HttpGet]
    [Route("GetBrandByIdFromHeader")]
    public IActionResult GetBrandByIdFromHeader([FromHeader] int id)
    {
        var brand = brands.Where(x => x.Id == id).FirstOrDefault();
        return Ok(brand);
    }
}
