using Carter;
using FitnessApp.Modules.Workout.Features.Workouts.Update;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Workouts;

public class UpdateWorkoutEndpoint:CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/workout/workouts/{id:guid}", async (Guid id,
            UpdateWorkoutRequest request, ISender sender) =>
        {
            await sender.Send(new UpdateWorkoutCommand(id, request.Name, request.Description));
            return Results.Ok(new
            {
                Message = "Workout updated successfully."
            });
        })
        .WithTags("Workout")
        .WithName("UpdateWorkout");
    }
}