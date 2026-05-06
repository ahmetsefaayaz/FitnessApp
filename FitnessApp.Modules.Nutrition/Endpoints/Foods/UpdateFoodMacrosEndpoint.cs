using Carter;
using FitnessApp.Modules.Nutrition.Features.Diets.Update;
using FitnessApp.Modules.Nutrition.Features.Foods.Update;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Foods;

public class UpdateFoodMacrosEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/nutrition/foods/{id:guid}",
                async (Guid id, UpdateFoodMacrosRequest request, ISender sender) =>
                {
                    await sender.Send(new UpdateFoodMacrosCommand(id, request.NewCalories, request.NewProtein,
                        request.NewCarbs, request.NewFats));
                    return Results.Ok(new
                    {
                        Message = "Successfully updated food macros"
                    });
                })
            .WithTags("Food")
            .WithName("UpdateFoodMacros");
    }
}