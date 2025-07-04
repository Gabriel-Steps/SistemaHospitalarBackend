using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SistemaHospitalar.Core.Exceptions;
using System.Text.Json;

namespace SistemaHospitalar.Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, $"Erro conhecido: {ex.Message}");
                await HandleExceptionAsync(context, ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno inesperado");
                await HandleExceptionAsync(context, 500, "Ocorreu um erro interno no servidor.");
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string mensagem)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new
            {
                Status = false,
                Mensagem = mensagem
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
