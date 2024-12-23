namespace CarApp.Exceptions;

public class ForbidAccessException : Exception
{
    public ForbidAccessException(string message) : base(message)
    {
    }

    public ForbidAccessException(string[] errors) : base($"Multiple errors occurred: {string.Join(", ", errors)}")
    {
        Errors = errors;
    }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string[]? Errors { get; set; }
}