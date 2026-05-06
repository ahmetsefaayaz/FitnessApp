using MediatR;

namespace FitnessApp.Modules.Identity.Features.Users.Register;

public record RegisterCommand(RegisterModel model): IRequest<Guid>;