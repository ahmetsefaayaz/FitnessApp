using Carter;
using FitnessApp.Modules.Workout.Features.Workouts.Create;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Workouts;

public class CreateWorkoutEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/workout/workouts", async (CreateWorkoutCommand command,  ISender sender) =>
        {
            var id = await sender.Send(command);
            return Results.Ok(new
            {
                Message = "Successfully created workout",
                WorkoutId = id
            });
        })
        .WithTags("Workout")
        .WithName("CreateWorkout")
        .RequireAuthorization();
    }
}