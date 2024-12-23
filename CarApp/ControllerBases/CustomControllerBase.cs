using DocvivoCrm.Framework.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DocvivoCrm.Framework.ControllerBases;

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