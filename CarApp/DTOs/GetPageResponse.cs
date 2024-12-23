namespace CarApp.DTOs;

public sealed record GetPageResponse<T>
{
    public GetPageResponse()
    {
        
    }

    public GetPageResponse(int totalCount, int pageCount, T[]? data)
    {
        TotalCount = totalCount;
        PageCount = pageCount;
        Data = data;
    }

    public int TotalCount { get; set; }
    public int PageCount { get; set; }
    public T[]? Data { get; set; }
}