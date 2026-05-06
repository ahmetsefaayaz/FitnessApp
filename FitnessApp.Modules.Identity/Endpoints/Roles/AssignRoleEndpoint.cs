using Carter;
using FitnessApp.Modules.Identity.Features.Roles.Roles;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Identity.Endpoints.Roles;

public class AssignRoleEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/identity/users/{id:guid}/roles", async (Guid id, AssignRoleRequest request, ISender sender) =>
        {
            await sender.Send(new AssignRoleCommand(id.ToString(),  request.Name));
            return Results.Ok(new
            {
                Message = "Role created"
            });
        })
        .WithTags("Role")
        .WithName("CreateRole")
        .RequireAuthorization("IdentityDepartmentOnly");
    }
}