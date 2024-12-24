using Microsoft.AspNetCore.Mvc;

namespace CarApp.DTOs.Common;

public abstract class SearchDto
{
    [FromQuery] public int PageIndex { get; set; } = 1;
    [FromQuery] public int PageSize { get; set; } = int.MaxValue;
    [FromQuery] public string SortColumn { get; set; } = "Id";
    [FromQuery] public bool SortDesc { get; set; } = true;
}