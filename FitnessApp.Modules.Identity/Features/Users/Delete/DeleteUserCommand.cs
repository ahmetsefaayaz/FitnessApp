using MediatR;

namespace FitnessApp.Modules.Identity.Features.Users.Delete;

public record DeleteUserCommand(Guid UserId, bool Permanent): IRequest;