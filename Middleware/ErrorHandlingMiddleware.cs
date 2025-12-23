using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace ContratosAPI.Middleware
{
    public class ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocorreu um erro não tratado");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string message = exception.Message;
            string? details = null;
            
            if (exception is DbUpdateException dbEx)
            {
                message = "Erro ao salvar dados no banco";
                details = dbEx.InnerException?.Message ?? dbEx.Message;

                // Tentar identificar erro específico
                if (details.Contains("foreign key", StringComparison.OrdinalIgnoreCase))
                {
                    message = "Referência inválida: Um ou mais IDs fornecidos não existem";
                }
                else if (details.Contains("unique", StringComparison.OrdinalIgnoreCase))
                {
                    message = "Já existe um registro com esse valor único";
                }
                else if (details.Contains("null", StringComparison.OrdinalIgnoreCase))
                {
                    message = "Campos obrigatórios estão faltando";
                }
            }

            string result = JsonSerializer.Serialize(new
            {
                error = message,
                details = details,
                type = exception.GetType().Name,
                stackTrace = exception.StackTrace
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}