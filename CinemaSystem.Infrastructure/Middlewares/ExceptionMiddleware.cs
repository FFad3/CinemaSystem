using CinemaSystem.Core.Exceptions;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CinemaSystem.Infrastructure.Middlewares
{
    internal sealed class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(ex, context);
            }
        }

        private async Task HandleExceptionAsync(Exception exception, HttpContext httpContext)
        {
            var normalizedExName = NormalizeExceptionName(exception);
            ProblemDetails problemDetails = exception switch
            {
                CustomValidationException ex => new ProblemDetails
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Title = "Validation Error",
                    Type = normalizedExName,
                    Errors = ex.Errors
                },
                _ => new ProblemDetails { Type = normalizedExName },
            };

            httpContext.Response.StatusCode = problemDetails.StatusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }

        private static string NormalizeExceptionName(Exception exception) => exception.GetType().Name.Underscore().Replace("_exception", string.Empty);
    }

    public sealed class ProblemDetails
    {
        public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;
        public string Title { get; set; } = "InternalServer error";
        public string Type { get; set; } = nameof(Exception);
        public IReadOnlyDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
        public string Details { get; set; } = default!;
    }
}