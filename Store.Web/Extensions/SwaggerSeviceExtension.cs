using Microsoft.OpenApi.Models;

namespace Store.Web.Extensions
{
    public static class SwaggerSeviceExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "Store Api 5",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "DamaStore Academy",
                        Email = "DamaStorecademy@gmil.com",
                        Url = new Uri("http://twitter.com/DamaStorecademy"),
                    }
                });

                var securiyySchem = new OpenApiSecurityScheme
                {
                   Description = "JWT Authorizatin header using the Bearer scheme . Example :\"Authorization : Bearer {token} \"",
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey,
                   Scheme = "bearer",
                   Reference = new OpenApiReference
                   {
                   Id = "bearer",
                   Type = ReferenceType.SecurityScheme,
                   }


                };
                options.AddSecurityDefinition("bearer", securiyySchem);

                var securityRequirements = new OpenApiSecurityRequirement
                {
                    {securiyySchem , new[] { "bearer" } }
                };
                options.AddSecurityRequirement(securityRequirements);
            });
            return services;
        }
    }
}
