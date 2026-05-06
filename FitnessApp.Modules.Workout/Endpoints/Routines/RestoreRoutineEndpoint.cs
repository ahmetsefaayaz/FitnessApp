using Carter;
using FitnessApp.Modules.Workout.Features.Routines.Restore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Routines;

public class RestoreRoutineEndpoint: CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/workout/routines/{id:guid}/restore", async (Guid id, ISender sender) =>
        {
            await sender.Send(new RestoreRoutineCommand(id));
            return Results.Ok(new
            {
                Message = "Routine restored successfully"
            });
        })
        .WithTags("Routine")
        .WithName("RestoreRoutine");
    }
}