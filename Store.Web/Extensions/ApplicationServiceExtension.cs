using Microsoft.AspNetCore.Mvc;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Services.ProductService.Dtos;
using Store.Service.Services.ProductService;
using Store.Service.HandleResponses;
using Store.Service.CaheSeervice;
using Store.Service.CaheService;

namespace Store.Web.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProuductService, ProuductService>();

            services.AddScoped<IcashService,cashService>();

            services.AddAutoMapper(typeof(ProductProfile));


            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                              .Where(model => model.Value?.Errors.Count > 0)
                                              .SelectMany(model => model.Value?.Errors)
                                              .Select(error => error.ErrorMessage)
                                              .ToList();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
