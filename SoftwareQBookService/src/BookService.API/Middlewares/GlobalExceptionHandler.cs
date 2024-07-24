using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace BookService.API.Middlewares;
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        _logger.LogError(
               exception,
               "Could not process a request on machine {Machine}. Traceid: {TraceId}",
               Environment.MachineName,
               traceId);

        var (statusCode, title, details) = MapException(exception);

        await Results.Problem(
               title: title, // can update error message.
               statusCode: statusCode,
               detail: details, // TODO: Only for development env, have to remove in production env
               extensions: new Dictionary<string, object?>
               {
                        {"traceId", Activity.Current?.Id }
               }
               ).ExecuteAsync(httpContext);

        return true;
    }

    private static (int StatusCode, string Title, string Details) MapException(Exception exception)
    {
        // Handle specific exception types with custom error codes and details
        return exception switch
        {
            ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, "Invalid argument", exception.Message),
            ArgumentNullException => (StatusCodes.Status400BadRequest, "Argument cannot be null", exception.Message),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized access", exception.Message),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found", exception.Message),
            // Add more specific exception types as needed
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred", exception.ToString())
        };
    }
}