using System.Net;
using System.Text.Json.Serialization;

namespace CarApp.DTOs;

public class Response<T>
{
    public T Data { get; set; }

    [JsonIgnore] public int StatusCode { get; set; }

    [JsonIgnore] public bool IsSuccessful { get; set; }

    public dynamic Errors { get; set; }

    // Static Factory Method
    public static Response<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new Response<T> { Data = data, StatusCode = (int)statusCode, IsSuccessful = true };
    }

    public static Response<T> Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new Response<T> { Data = default, StatusCode = (int)statusCode, IsSuccessful = true };
    }

    public static Response<T> Fail(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)

    {
        return new Response<T>
        {
            Errors = errors,
            StatusCode = (int)statusCode,
            IsSuccessful = false
        };
    }

    public static Response<T> Fail(string error, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        return new Response<T> { Errors = new List<string> { error }, StatusCode = (int)statusCode, IsSuccessful = false };
    }
    
    public static Response<T> Fail(dynamic errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        return new Response<T> { Errors = errors, StatusCode = (int)statusCode, IsSuccessful = false };
    }
}