using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using CarApp.Errors;
using CarApp.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CarApp.Extensions;

[ExcludeFromCodeCoverage]
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        _ = app.UseExceptionHandler(appError => appError.Run(async context =>
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature != null)
            {
                // Set the Http Status Code
                var statusCode = contextFeature.Error switch
                {
                    ValidationException => HttpStatusCode.UnprocessableEntity,
                    NotFoundException => HttpStatusCode.NotFound,
                    BadRequestException => HttpStatusCode.BadRequest,
                    BadHttpRequestException => HttpStatusCode.BadRequest,
                    UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    ForbidAccessException => HttpStatusCode.Forbidden,
                    _ => HttpStatusCode.InternalServerError
                };

                switch (contextFeature.Error)
                {
                    case ValidationException error:
                        var validationError = new ValidationError(error.Message,
                            error.Errors);

                        // Set Response Details
                        context.Response.StatusCode = (int)statusCode;
                        context.Response.ContentType = "application/json";

                        // Return the Serialized Validation Error
                        await context.Response.WriteAsync(JsonSerializer.Serialize(validationError));
                        break;
                    default:
                        // Prepare Generic Error
                        var apiError = new ApiError(contextFeature.Error.Message);

                        if (statusCode is HttpStatusCode.InternalServerError)
                        {
                            apiError.StackTrace = contextFeature.Error.StackTrace!;
                            apiError.InnerMessage = contextFeature.Error.InnerException?.Message!;
                        }
                        else if (statusCode is HttpStatusCode.Unauthorized)
                        {
                            apiError.InnerMessage = contextFeature.Error.InnerException?.Message!;
                        }

                        // Set Response Details
                        context.Response.StatusCode = (int)statusCode;
                        context.Response.ContentType = "application/json";

                        // Return the Serialized Generic Error
                        await context.Response.WriteAsync(JsonSerializer.Serialize(apiError));
                        break;
                }
            }
        }));

        return app;
    }
}