using Carter;
using FitnessApp.Modules.Nutrition.Features.Diets.Restore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Diets;

public class RestoreDietEndpoint: CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/nutrition/diets/{id:guid}/restore", async (Guid id, ISender sender) =>
        {
            await sender.Send(new RestoreDietCommand(id));
            return Results.Ok(new
            {
                Message = "Restored Diet Successfully"
            });
        })
        .WithTags("Diet")
        .WithName("RestoreDiet")
        .RequireAuthorization("NutritionDepartmentOnly");
    }
}