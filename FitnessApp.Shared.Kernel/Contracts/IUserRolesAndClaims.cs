namespace FitnessApp.Shared.Kernel.Contracts;

public interface IUserRolesAndClaims
{
    bool HasClaim(string claimType, string claimValue);
    bool HasRole(string role);
}