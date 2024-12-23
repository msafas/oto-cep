namespace CarApp.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string[] errors) : base($"Multiple errors occurred: {string.Join(", ", errors)}")
    {
        Errors = errors;
    }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string[]? Errors { get; set; }
}