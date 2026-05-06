using FitnessApp.Modules.Identity.Entities;
using FitnessApp.Shared.Kernel.Contracts;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Modules.Identity.Features.Users.Delete;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRolesAndClaims _userRolesAndClaims;

    public DeleteUserCommandHandler(UserManager<User> userManager,  IUserRolesAndClaims userRolesAndClaims)
    {
        _userManager = userManager;
        _userRolesAndClaims = userRolesAndClaims;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) throw new NotFoundException("No user found");
    
        if (request.Permanent)
        {
            if (!_userRolesAndClaims.HasRole("Admin"))
                throw new UnauthorizedAccessException("You must be an Admin to hard delete a user.");
            await _userManager.DeleteAsync(user);
        }
        else
        {
            user.SoftDelete();
            await _userManager.UpdateAsync(user);
        }
    }
}