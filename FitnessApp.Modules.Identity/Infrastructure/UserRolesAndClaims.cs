using FitnessApp.Shared.Kernel.Contracts;
using Microsoft.AspNetCore.Http;

namespace FitnessApp.Modules.Identity.Infrastructure;

public class UserRolesAndClaims: IUserRolesAndClaims
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRolesAndClaims(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public bool HasClaim(string claimType, string claimValue) =>
    _httpContextAccessor.HttpContext?.User?.HasClaim(claimType, claimValue) ?? false;
    public bool HasRole(string role) =>
    _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
}