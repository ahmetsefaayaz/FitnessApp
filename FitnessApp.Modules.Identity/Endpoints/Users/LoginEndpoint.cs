using Carter;
using FitnessApp.Modules.Identity.Features.Users.Login;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Identity.Endpoints.Users;

public class LoginEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/identity/users/login", async (LoginCommand command, ISender sender) =>
            {
                var token = sender.Send(command);
                return Results.Ok(new
                {
                    Token = token.Result
                });
            })
            .WithTags("Authentication")
            .WithName("Login");
    }
}