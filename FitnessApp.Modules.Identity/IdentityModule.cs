using System.Text;
using FitnessApp.Modules.Identity.Entities;
using FitnessApp.Modules.Identity.Features.Users.Login;
using FitnessApp.Modules.Identity.Infrastructure;
using FitnessApp.Shared.Kernel.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FitnessApp.Modules.Identity;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FitnessIdentityDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("IdentityConnection")));
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(IdentityModule).Assembly);
        });
        services.AddDataProtection();
        
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IIdentityModuleApi, IdentityModuleApi>();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserRolesAndClaims, UserRolesAndClaims>();
        
        services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<FitnessIdentityDbContext>() 
            .AddDefaultTokenProviders();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!))
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly",  policy => policy.RequireRole("Admin"));
            
            options.AddPolicy("IdentityDepartmentOnly", policy =>
            {
                policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin")||
                    context.User.HasClaim(c => c.Type == "Department" && c.Value == "Identity"));
            });
            options.AddPolicy("WorkoutDepartmentOnly", policy =>
            {
                policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin")||
                    context.User.HasClaim(c => c.Type == "Department" && c.Value == "Workout"));
            });
            options.AddPolicy("NutritionDepartmentOnly", policy =>
            {
                policy.RequireAssertion(context => 
                    context.User.IsInRole("Admin") ||
                    context.User.HasClaim(c => c.Type == "Department" && c.Value == "Nutrition"));
            });
        });
        
        
        return services;
        
    }
    public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FitnessIdentityDbContext>();
        context.Database.Migrate();
        
        return app;
    }
}