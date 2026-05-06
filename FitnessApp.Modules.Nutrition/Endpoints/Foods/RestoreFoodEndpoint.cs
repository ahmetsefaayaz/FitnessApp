using Carter;
using FitnessApp.Modules.Nutrition.Features.Foods.Restore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Nutrition.Endpoints.Foods;

public class RestoreFoodEndpoint: CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/nutrition/foods/{id:guid}/restore", async (Guid id, ISender sender) =>
        {
            await sender.Send(new RestoreFoodCommand(id));
            return Results.Ok(new
            {
                Message = "Food restored successfully"
            });
        })
        .WithTags("Food")
        .WithName("RestoreFood");
    }
}