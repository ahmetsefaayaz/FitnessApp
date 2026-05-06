using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FitnessApp.Modules.Identity.Entities;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FitnessApp.Modules.Identity.Features.Users.Login;

internal sealed class LoginCommandHandler: IRequestHandler<LoginCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtProvider _jwtProvider;
    public LoginCommandHandler(UserManager<User> userManager,
        IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }
    
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) throw new NotFoundException("User not found");
        if (!_userManager.CheckPasswordAsync(user, request.Password).Result)
        {
            throw new WrongInputException("Wrong password");
        }

        return await _jwtProvider.GenerateTokenAsync(user);
    }

}