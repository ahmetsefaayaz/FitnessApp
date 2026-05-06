using Carter;
using FitnessApp.Modules.Nutrition.Features.Diets.Create;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Diets;

public class CreateDietEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/nutrition/diets", async (CreateDietCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(result);
        })
        .WithTags("Diet")
        .WithName("CreateDiet")
        .RequireAuthorization("NutritionDepartmentOnly");
    }
}