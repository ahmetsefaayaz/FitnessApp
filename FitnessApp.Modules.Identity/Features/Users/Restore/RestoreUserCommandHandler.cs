using FitnessApp.Modules.Identity.Entities;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Modules.Identity.Features.Users.Restore;

public class RestoreUserCommandHandler: IRequestHandler<RestoreUserCommand>
{
    private readonly UserManager<User> _userManager;

    public RestoreUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task Handle(RestoreUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) throw new NotFoundException("User not found");
        user.Restore();
        await _userManager.UpdateAsync(user);
    }
}