using System.Diagnostics.CodeAnalysis;

namespace CarApp.Errors;

[ExcludeFromCodeCoverage]
public class ApiError
{
    public ApiError(string message, string? innerMessage, string? stackTrace)
    {
        Message = message;
        InnerMessage = innerMessage;
        StackTrace = stackTrace;
    }

    public ApiError(string message)
    {
        Message = message;
    }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string Message { get; set; }
    public string? InnerMessage { get; set; }
    public string? StackTrace { get; set; }
}