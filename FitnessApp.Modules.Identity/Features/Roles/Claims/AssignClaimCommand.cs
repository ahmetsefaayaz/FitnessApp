using MediatR;

namespace FitnessApp.Modules.Identity.Features.Roles.Claims;

public record AssignClaimCommand(string UserId, string Type, string Value): IRequest;