using Serilog;
using System.Net;
using System.Text.Json;

namespace ProductCatalog.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int status;
            string message;

            switch (exception)
            {
                //Сюда можно еще всякого добавить, но пока тут самое необходимое.
                case InvalidOperationException:
                    status = (int)HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                default:
                    status = (int)HttpStatusCode.InternalServerError;
                    message = "Internal server error";
                    Log.Error(exception.Message + "\n===StackTrace===\n" + exception.StackTrace);
                    break;
            }

            context.Response.ContentType = JsonContentType;
            context.Response.StatusCode = status;

            await context.Response.WriteAsync(JsonSerializer.Serialize(message));
        }
    }
}
