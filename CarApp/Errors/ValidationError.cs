using System.Diagnostics.CodeAnalysis;

namespace CarApp.Errors;

[ExcludeFromCodeCoverage]
public class ValidationError
{
    public ValidationError(string message, IDictionary<string, string[]> errors)
    {
        Message = message;
        Errors = errors;
    }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global

    public string Message { get; set; }
    public IDictionary<string, string[]> Errors { get; set; }
}