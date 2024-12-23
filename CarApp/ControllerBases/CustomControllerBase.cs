using CarApp.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarApp.ControllerBases;

[ApiController]
[Route("api/[controller]")]
public class CustomControllerBase : ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(Response<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}