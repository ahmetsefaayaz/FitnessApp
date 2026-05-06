using MediatR;

namespace FitnessApp.Modules.Identity.Features.Roles.Roles;

public record AssignRoleCommand(string Id, string Name): IRequest;