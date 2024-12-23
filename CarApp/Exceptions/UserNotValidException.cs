using CarApp.Enums;

namespace CarApp.Exceptions;

public class UserNotValidException() : Exception(Message)
{
    private new static readonly string Message = $"{(int)AuthExceptions.UserNotValid}";
}