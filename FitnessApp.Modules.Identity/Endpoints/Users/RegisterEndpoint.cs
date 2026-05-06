using Carter;
using FitnessApp.Modules.Identity.Features.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Identity.Endpoints.Users;

public class RegisterEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/identity/users/register", async (RegisterCommand command, ISender sender) =>
            {
                var userId = await sender.Send(command);
                return Results.Ok(new
                {
                    Message = "Successfully registered user",
                    UserId = userId
                });
            })
            .WithTags("Authentication")
            .WithName("Register");
    }
}