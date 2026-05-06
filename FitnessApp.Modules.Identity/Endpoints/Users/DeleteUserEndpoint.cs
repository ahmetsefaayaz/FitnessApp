using Carter;
using FitnessApp.Modules.Identity.Features.Users.Delete;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Identity.Endpoints.Users;

public class DeleteUserEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/identity/users/{id:guid}", async (Guid id, [FromQuery] bool permanent,
                ISender sender) =>
            {
                await sender.Send(new DeleteUserCommand(id, permanent));
                return Results.Ok(new
                {
                    Message = "Successfully deleted user"
                });
            })
            .WithName("DeleteUser")
            .WithTags("User")
            .RequireAuthorization("IdentityDepartmentOnly");
    }
}