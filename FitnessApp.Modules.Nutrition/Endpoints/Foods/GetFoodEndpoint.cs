using Carter;
using FitnessApp.Modules.Nutrition.Features.Foods.Get;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Foods;

public class GetFoodEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/nutrition/foods/{id:guid}", async (Guid id, ISender sender) =>
            {
                var food = await sender.Send(new GetFoodQuery(id));
                return Results.Ok(food);
            })
            .WithTags("Food")
            .WithName("GetFood")
            .RequireAuthorization("AdminOnly");
        app.MapGet("/api/nutrition/foods", async (ISender sender) =>
        {
            var foods = await sender.Send(new GetFoodsQuery());
            return Results.Ok(foods);
        })
        .WithTags("Food")
        .WithName("GetFoods")
        .RequireAuthorization("AdminOnly");
    }
    
}