using Microsoft.AspNetCore.Mvc;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Services.ProductService.Dtos;
using Store.Service.Services.ProductService;
using Store.Service.HandleResponses;
using Store.Service.Services.CaheService;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.BasketService;
using Store.Repository.Basket;
using Store.Service.Services.TokenService;
using Store.Service.Services.UserService;

namespace Store.Web.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProuductService, ProuductService>();
            services.AddScoped<IcashService,cashService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));


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
