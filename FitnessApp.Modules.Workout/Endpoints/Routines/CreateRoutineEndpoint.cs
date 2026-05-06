using Carter;
using FitnessApp.Modules.Workout.Features.Routines.Create;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Workout.Endpoints.Routines;

public class CreateRoutineEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/workout/routines", async (CreateRoutineCommand command, ISender sender) =>
            {
                var routineId = await sender.Send(command);
            
                return Results.Ok(new 
                { 
                    Id = routineId, 
                    Message = "Rutin başarıyla oluşturuldu!" 
                });
            })
            .WithTags("Routine")
            .WithName("CreateRoutine")
            .RequireAuthorization("WorkoutDepartmentOnly"); 
    }
}