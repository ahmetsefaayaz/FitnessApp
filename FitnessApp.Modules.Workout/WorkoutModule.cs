using FitnessApp.Modules.Workout.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.Modules.Workout;

public static class WorkoutModule
{
    public static IServiceCollection AddWorkoutModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<WorkoutDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("WorkoutConnection")));
        services.AddMediatR(config => 
        {
            config.RegisterServicesFromAssembly(typeof(WorkoutModule).Assembly);
        });
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
            options.InstanceName = "FitnessApp_";
        });
        return services;
    }
    public static IApplicationBuilder UseWorkoutModule(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WorkoutDbContext>();
        context.Database.Migrate();
        
        return app;
    }
}