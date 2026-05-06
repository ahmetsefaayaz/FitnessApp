using FitnessApp.Modules.Identity.Entities;
using FitnessApp.Shared.Kernel.Contracts;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Modules.Identity.Infrastructure;

internal sealed class IdentityModuleApi: IIdentityModuleApi
{
    private readonly UserManager<User> _userManager;

    public IdentityModuleApi(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user != null;
    }
}