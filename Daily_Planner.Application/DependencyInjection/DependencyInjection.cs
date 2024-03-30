using Daily_Planner.Application.Mapping;
using Daily_Planner.Application.Services;
using Daily_Planner.Application.Validations;
using Daily_Planner.Application.Validations.FluentValidations.Report;
using Daily_Planner.Domain.Dto.Report;
using Daily_Planner.Domain.Interfaces.Services;
using Daily_Planner.Domain.Interfaces.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Daily_Planner.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ReportMapping));
        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IReportvalidator, ReportValidator>();
        services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
        services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
        
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IAuthServices, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}