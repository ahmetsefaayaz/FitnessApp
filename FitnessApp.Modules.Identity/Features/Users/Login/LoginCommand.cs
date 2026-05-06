using MediatR;

namespace FitnessApp.Modules.Identity.Features.Users.Login;

public record LoginCommand(string Email, string Password) : IRequest<string>;