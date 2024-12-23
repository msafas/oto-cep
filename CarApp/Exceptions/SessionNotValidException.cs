using CarApp.Enums;

namespace CarApp.Exceptions;

public class SessionNotValidException() : Exception(Message)
{
    private new static readonly string Message = $"{(int)AuthExceptions.SessionNotValid}";
}