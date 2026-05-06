using Carter;
using FitnessApp.Modules.Workout.Features.Routines.Get;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Routines;

public class GetRoutineEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/workout/routines/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetRoutineQuery(id);
            var routine = await sender.Send(query);
            return Results.Ok(routine);
        })
        .WithTags("Routine")
        .WithName("GetRoutine")
        .RequireAuthorization("WorkoutDepartmentOnly");
    }
}