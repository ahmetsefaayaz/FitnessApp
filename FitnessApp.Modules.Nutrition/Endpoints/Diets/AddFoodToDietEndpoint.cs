using Carter;
using FitnessApp.Modules.Nutrition.Features.Diets.AddFood;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Diets;

public class AddFoodToDietEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/nutrition/diets/{id:guid}/foods", async (Guid id, AddFoodToDietRequest request, ISender sender) =>
            {
                var foodItemId = await sender.Send(new AddFoodToDietCommand(id, request.FoodId, request.Amount));
                return Results.Ok(foodItemId);
            })
            .WithTags("Diet")
            .WithName("AddFoodToDiet")
            .RequireAuthorization("NutritionDepartmentOnly");
    }
}