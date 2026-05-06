using Carter;
using FitnessApp.Modules.Identity.Features.Roles.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FitnessApp.Modules.Identity.Endpoints.Roles;

public class AssignClaimEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/identity/users/{id:guid}/claims", async (Guid id, AssignClaimRequest request, ISender sender) =>
        {
            await sender.Send(new AssignClaimCommand(id.ToString(), request.Type, request.Value));
            return Results.Ok(new
            {
                Message = "Claim created successfully"
            });
        })
        .WithTags("Role")
        .WithName("CreateClaim")
        .RequireAuthorization("IdentityDepartmentOnly");
    }
}