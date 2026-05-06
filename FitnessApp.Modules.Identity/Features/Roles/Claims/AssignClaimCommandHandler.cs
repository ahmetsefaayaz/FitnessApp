using System.Security.Claims;
using FitnessApp.Modules.Identity.Entities;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Modules.Identity.Features.Roles.Claims;

public class AssignClaimCommandHandler: IRequestHandler<AssignClaimCommand>
{
    private readonly UserManager<User> _userManager;
    public AssignClaimCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task Handle(AssignClaimCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null) throw new NotFoundException("User not found");
        var claims = _userManager.GetClaimsAsync(user).Result;
        if (claims.Any(c => c.Type == request.Type && c.Value == request.Value))
        {
            throw new WrongInputException("User already has this claim");
        }
        var claim = new Claim(request.Type, request.Value);
        await _userManager.AddClaimAsync(user, claim);
        
    }
}