using Carter;
using FitnessApp.Modules.Workout.Features.Routines.Delete;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Routines;

public class DeleteRoutineEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/workout/routines/{id:guid}", async (Guid id, 
                [FromQuery] bool permanent, ISender sender) =>
        {
            await sender.Send(new DeleteRoutineCommand(id, permanent));
            return Results.Ok(new
            {
                Message = "Successfully deleted Routine"
            });
        })
        .WithTags("Routine")
        .WithName("DeleteRoutine")
        .RequireAuthorization("WorkoutDepartmentOnly");
    }
}