using FitnessApp.Modules.Identity.Entities;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Modules.Identity.Features.Roles.Roles;

public class AssignRoleCommandHandler: IRequestHandler<AssignRoleCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    public AssignRoleCommandHandler(UserManager<User> userManager,  RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user == null) throw new NotFoundException("user not found");
        var role = await _roleManager.FindByNameAsync(request.Name);
        if (role == null) throw new NotFoundException("role not found");
        if (_userManager.GetRolesAsync(user).Result.Contains(request.Name))
            throw new WrongInputException("user already has this role");
        
        await _userManager.AddToRoleAsync(user, request.Name);
    }
}