using Carter;
using FitnessApp.Modules.Workout.Features.Workouts.Get;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Workouts;

public class GetWorkoutEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/workout/workouts", async (ISender sender) =>
            {
                var workouts = await sender.Send(new GetAllWorkoutsQuery());
                return Results.Ok(workouts);
            })
            .WithName("GetWorkouts")
            .WithTags("Workout")
            .RequireAuthorization("WorkoutDepartmentOnly");
        app.MapGet("api/workout/workouts/{id:guid}", async (Guid id, ISender sender) =>
            {
                var workout = await sender.Send(new GetWorkoutQuery(id));
                return Results.Ok(workout);
            })
            .WithTags("Workout")
            .WithName("GetWorkout")
            .RequireAuthorization("WorkoutDepartmentOnly");
    }
}