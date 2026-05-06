using Carter;
using FitnessApp.Modules.Workout.Features.Workouts.Restore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Workouts;

public class RestoreWorkoutEndpoint: CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/workout/workouts/{id:guid}/restore", async (Guid id, ISender sender) =>
        {
            await sender.Send(new RestoreWorkoutCommand(id));
            return Results.Ok(new
            {
                Message = "Workout restored successfully"
            });
        })
        .WithTags("Workout")
        .WithName("RestoreWorkout")
        .RequireAuthorization("WorkoutDepartmentOnly");
    }
}