using CarApp.Enums;

namespace CarApp.Exceptions;

public class JtiNotValidException() : Exception(Message)
{
    private new static readonly string Message = $"{(int)AuthExceptions.JtiNotValid}";
}