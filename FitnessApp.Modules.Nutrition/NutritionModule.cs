using FitnessApp.Modules.Nutrition.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.Modules.Nutrition;

public static class NutritionModule
{
    public static IServiceCollection AddNutritionModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NutritionDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("NutritionConnection")));
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(NutritionModule).Assembly);
        });

        return services;
    }
    public static IApplicationBuilder UseNutritionModule(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NutritionDbContext>();
        context.Database.Migrate();
        
        return app;
    }
}