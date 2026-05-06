using FitnessApp.Modules.Identity.Entities;

namespace FitnessApp.Modules.Identity.Features.Users.Login;

public interface IJwtProvider
{
    Task<string> GenerateTokenAsync(User user);
}