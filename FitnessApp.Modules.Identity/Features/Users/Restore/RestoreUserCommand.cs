using MediatR;

namespace FitnessApp.Modules.Identity.Features.Users.Restore;

public record RestoreUserCommand(Guid UserId): IRequest;