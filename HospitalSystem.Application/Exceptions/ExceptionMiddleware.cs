using FluentValidation;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Auth.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;
using Serilog.Core;
using System.Text.Json;


namespace HospitalSystem.Application.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            int statusCode = GetStatusCode(exception);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            if (exception.GetType() == typeof(ValidationException))
                return httpContext.Response.WriteAsync(new ExceptionModel
                {
                    Errors = ((ValidationException)exception).Errors.Select(x => x.ErrorMessage),
                    StatusCode = StatusCodes.Status400BadRequest
                }.ToString());

            List<string> errors = new()
            {
                $"Error message : {exception.Message}"
            };

            return httpContext.Response.WriteAsync(new ExceptionModel
            {
                Errors = errors,
                StatusCode = statusCode
            }.ToString());

        }

        private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ValidationException => StatusCodes.Status422UnprocessableEntity,

            EmailOrPasswordShouldNotBeInvalidException => StatusCodes.Status401Unauthorized,

            BaseException => StatusCodes.Status400BadRequest,

            _ => StatusCodes.Status500InternalServerError
        };

    }
}
