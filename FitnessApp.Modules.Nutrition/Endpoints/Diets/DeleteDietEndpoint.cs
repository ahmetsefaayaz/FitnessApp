using Carter;
using FitnessApp.Modules.Nutrition.Features.Diets.Delete;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Diets;

public class DeleteDietEndpoint: CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/nutrition/diets/{id:guid}", async (Guid id,[FromQuery] bool permanent, ISender sender) =>
            {
                await sender.Send(new DeleteDietCommand(id, permanent));
                return Results.Ok(new
                {
                    Message = "Successfully deleted diet"
                });
            })
            .WithTags("Diet")
            .WithName("DeleteDiet")
            .RequireAuthorization("NutritionDepartmentOnly");
    }
}