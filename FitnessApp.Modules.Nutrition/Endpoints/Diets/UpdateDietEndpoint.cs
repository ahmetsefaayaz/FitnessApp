using Carter;
using FitnessApp.Modules.Nutrition.Features.Diets.Update;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Diets;

public class UpdateDietEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/nutrition/diets/{dietId:guid}/foods/{foodId:guid}", async (Guid dietId,
                Guid foodId, UpdateFoodAmountRequest request,
                ISender sender) =>
            {
                await sender.Send(new UpdateFoodAmountCommand(dietId, foodId, request.Amount));
                return Results.Ok(new
                {
                    Message = "Successfully updated diet"
                });
            })
            .WithTags("Diet")
            .WithName("UpdateDietItem")
            .RequireAuthorization("NutritionDepartmentOnly");
        app.MapPut("/api/nutrition/diets/{id:guid}", async (Guid id, UpdateDietTitleRequest request,
                ISender sender) =>
            {
                await sender.Send(new UpdateDietTitleCommand(id, request.Title));
                return Results.Ok(new
                {
                    Message = "Successfully updated diet"
                });
            })
            .WithTags("Diet")
            .WithName("UpdateDietTitle")
            .RequireAuthorization("NutritionDepartmentOnly");
    }
}