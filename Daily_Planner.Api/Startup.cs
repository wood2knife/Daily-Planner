using System.Reflection;
using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace Daily_Planner.Api;

public static class Startup
{
    /// <summary>
    /// Подключение Swagger
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning()
            .AddApiExplorer(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "Daily_Planner.API",
                Description = "Version 1.0",
                TermsOfService = new Uri("https://github.com/wood2knife/Daily-Planner"),
                Contact = new OpenApiContact()
                {
                    Name = "wood2knife",
                    Url = new Uri("https://github.com/wood2knife/Daily-Planner")
                }
            });
            
            options.SwaggerDoc("v2", new OpenApiInfo()
            {
                Version = "v2",
                Title = "Daily_Planner.API",
                Description = "Version 2.0",
                TermsOfService = new Uri("https://github.com/wood2knife/Daily-Planner"),
                Contact = new OpenApiContact()
                {
                    Name = "wood2knife",
                    Url = new Uri("https://github.com/wood2knife/Daily-Planner")
                }
            });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Description = "Enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
    }
}