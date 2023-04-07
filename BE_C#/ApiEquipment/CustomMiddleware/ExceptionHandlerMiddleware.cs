using ApiEquipment.Helpers;
using Newtonsoft.Json;
using System.Net;

namespace ApiEquipment.CustomMiddleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred: {ex}");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var respone = new Respone
                {
                    Status = false,
                    Message = "An unexpected error occurred while processing your request(Custom)"
                };

                var jsonErrorDetails = JsonConvert.SerializeObject(respone);
                await context.Response.WriteAsync(jsonErrorDetails);
            }
        }
    }

}
