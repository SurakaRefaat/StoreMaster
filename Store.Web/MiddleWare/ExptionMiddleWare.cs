using Store.Service.HandleResponses;
using System.Net;
using System.Text.Json;

namespace Store.Web.MiddleWare
{
    public class ExptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<ExptionMiddleWare> _logger;

        public ExptionMiddleWare(RequestDelegate next ,IHostEnvironment environment,ILogger<ExptionMiddleWare> logger)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex ,ex.Message);

                context.Response.ContentType = "applicatio/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500

                var response = _environment.IsDevelopment()
                    ? new CustomException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new CustomException((int)HttpStatusCode.InternalServerError);

                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, option);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
