using Carter;
using FitnessApp.Modules.Workout.Features.Routines.AddWorkout;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Routines;

public class AddWorkoutToRoutineEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/workout/routines/{id:guid}/workouts", async (Guid id,
                AddWorkoutToRoutineRequest request, ISender sender) =>
        {
            
            await sender.Send(new AddWorkoutToRoutineCommand(id, request.WorkoutId));
            return Results.Ok(new
            {
                Message = "Antrenman rutine başarıyla eklendi"
            });
        })
        .WithTags("Routine")
        .WithName("AddWorkoutToRoutine")
        .RequireAuthorization("WorkoutDepartmentOnly");
    }
}