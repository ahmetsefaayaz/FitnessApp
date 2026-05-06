using Carter;
using FitnessApp.Modules.Workout.Features.Routines.Update;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Routines;

public class UpdateRoutineEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/workout/routines/{id:guid}",
            async (Guid id, UpdateRoutineRequest request, ISender sender) =>
            {
                await sender.Send(new UpdateRoutineCommand(id, request.Name));
                return Results.Ok(new
                {
                    Message = "Successfully updated routine"
                });
            })
            .WithTags("Routine")
            .WithName("UpdateRoutine")
            .RequireAuthorization("WorkoutDepartmentOnly");
    }
}