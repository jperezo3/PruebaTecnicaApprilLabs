namespace ApiProductos.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Continua con la ejecución del siguiente middleware
            }
            catch (KeyNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await EnviarRespuestaError(context, "Recurso no encontrado", ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await EnviarRespuestaError(context, "Error interno del servidor", ex.Message);
            }
        } 

        private async Task EnviarRespuestaError(HttpContext context, string mensaje, string detalle)
        {
            context.Response.ContentType = "application/json";
            var errorResponse = new { message = mensaje, detail = detalle };
            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}

