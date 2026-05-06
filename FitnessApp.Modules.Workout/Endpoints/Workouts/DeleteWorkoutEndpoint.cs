using Carter;
using FitnessApp.Modules.Workout.Features.Workouts.Delete;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Workouts;

public class DeleteWorkoutEndpoint: CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/workout/workouts/{id:guid}", async (Guid id,
                [FromQuery] bool permanent, ISender sender) =>
        {
            await sender.Send(new DeleteWorkoutCommand(id, permanent));
            return Results.Ok(new
            {
                Message = "Workout deleted"
            });

        })
        .WithTags("Workout")
        .WithName("DeleteWorkout")
        .RequireAuthorization("WorkoutDepartmentOnly");
    }
}