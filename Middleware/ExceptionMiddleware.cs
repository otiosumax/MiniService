using MiniService.Models;

namespace MiniService.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            await HandleException(context, ex.Message, "validation_error", 400);
        }
        catch (KeyNotFoundException ex)
        {
            await HandleException(context, ex.Message, "not_found", 404);
        }
        catch (Exception)
        {
            await HandleException(context, "Internal server error", "internal_error", 500);
        }
    }

    private static async Task HandleException(
        HttpContext context,
        string message,
        string code,
        int statusCode)
    {
        var error = new ErrorResponse
        {
            Code = code,
            Message = message,
            RequestId = context.TraceIdentifier
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(error);
    }
}