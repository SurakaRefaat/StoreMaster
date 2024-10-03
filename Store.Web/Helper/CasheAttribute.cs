using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.CaheSeervice;
using System.Text;

namespace Store.Web.Helper
{
    public class CasheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSecondes;

        public CasheAttribute(int TimeToLiveInSecondes)
        {
            _timeToLiveInSecondes = TimeToLiveInSecondes;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _caheService = context.HttpContext.RequestServices.GetRequiredService<IcashService>();

            var caheKey = GeneratCacheKeyFromRequest(context.HttpContext.Request);

            var chachedRespone = await _caheService.GetCashResponeAsync(caheKey);

            if (!string.IsNullOrEmpty(chachedRespone))
            {
                var contentResut = new ContentResult
                {
                    Content = chachedRespone,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResut;

                return;
            }
            else
            {
               var exexutecContext = await next();
                if (exexutecContext.Result is OkObjectResult response)
                {
                    await _caheService.SetCashResponseAsync(caheKey,response.Value, TimeSpan.FromSeconds(_timeToLiveInSecondes));
                }
            }

        }
        private string GeneratCacheKeyFromRequest(HttpRequest request)
        {
            StringBuilder caheKey = new StringBuilder();
            caheKey.Append($"{request.Path}");

            foreach (var (Key, Value) in request.Query.OrderBy(X => X.Key))
                caheKey.Append($"|{Key}-{Value}");

            return caheKey.ToString();
        }
    }
}
