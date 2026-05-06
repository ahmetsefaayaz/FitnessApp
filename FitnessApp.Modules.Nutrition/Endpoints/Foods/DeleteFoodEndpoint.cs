using Carter;
using FitnessApp.Modules.Nutrition.Features.Foods.Delete;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Foods;

public class DeleteFoodEndpoint:CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/nutrition/foods/{id:guid}", async (Guid id,[FromQuery] bool permanent, ISender sender) =>
        {
            await sender.Send(new DeleteFoodCommand(id, permanent));
            return Results.Ok(new
            {
                Message = "Successfully deleted food"
            });
        })
        .WithTags("Food")
        .WithName("DeleteFood")
        .RequireAuthorization("NutritionDepartmentOnly");
    }
}