using Carter;
using FitnessApp.Modules.Workout.Features.Routines.Get;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Routines;

public class GetAllRoutinesEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/workout/routines", async (ISender sender) =>
            {
                var routines = await sender.Send(new GetAllRoutinesQuery());
                return Results.Ok(routines);
            })
            .WithName("GetAllRoutines")
            .WithTags("Routine")
            .RequireAuthorization("WorkoutDepartmentOnly");
    }
}