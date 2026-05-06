using Carter;
using FitnessApp.Modules.Identity;
using FitnessApp.Modules.Identity.Entities;
using FitnessApp.Modules.Identity.Infrastructure;
using FitnessApp.Modules.Nutrition;
using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Modules.Workout;
using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/FitnessLog-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Lütfen oluşturduğunuz JWT Token'ı aşağıdaki kutucuğa yapıştırın."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddWorkoutModule(builder.Configuration);
builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddNutritionModule(builder.Configuration);
builder.Services.AddCarter();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();


app.UseWorkoutModule();
app.UseIdentityModule();
app.UseNutritionModule();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<Role>>();
        
        await IdentitySeeder.SeedAsync(roleManager, userManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Veritabanı tohumlanırken bir hata oluştu: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapCarter();

app.Run();