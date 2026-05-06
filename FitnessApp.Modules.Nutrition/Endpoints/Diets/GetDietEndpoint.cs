using Carter;
using FitnessApp.Modules.Nutrition.Features.Diets.Get;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Diets;

public class GetDietEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/nutrition/diets", async (ISender sender) =>
        {
            var diets = await sender.Send(new  GetDietsQuery());
            return Results.Ok(diets);
        })
        .WithTags("Diet")
        .WithName("GetDiets")
        .RequireAuthorization("NutritionDepartmentOnly");
        app.MapGet("/api/nutrition/diets/{id:guid}", async (Guid id, ISender sender) =>
        {
            var diet = await sender.Send(new GetDietQuery(id));
            return Results.Ok(diet);
        })
        .WithTags("Diet")
        .WithName("GetDiet")
        .RequireAuthorization("NutritionDepartmentOnly");
    }
}