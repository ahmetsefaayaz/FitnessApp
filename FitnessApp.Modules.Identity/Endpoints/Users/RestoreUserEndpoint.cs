using Carter;
using FitnessApp.Modules.Identity.Features.Users.Restore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Identity.Endpoints.Users;

public class RestoreUserEndpoint:CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/identity/users/{id:guid}/restore", async (Guid id, ISender sender) =>
            {
                await sender.Send(new RestoreUserCommand(id));
                return Results.Ok(new
                {
                    Message = "User restored successfully"
                });
            })
            .WithTags("User")
            .WithName("RestoreUser")
            .RequireAuthorization("IdentityDepartmentOnly");
    }
}