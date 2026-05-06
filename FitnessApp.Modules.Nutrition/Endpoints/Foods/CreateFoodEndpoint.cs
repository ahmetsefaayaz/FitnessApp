using Carter;
using FitnessApp.Modules.Nutrition.Features.Foods.Create;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Foods;

public class CreateFoodEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/nutrition/foods", async (CreateFoodCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithTags("Food")
            .WithName("CreateFood")
            .RequireAuthorization();
    }
}