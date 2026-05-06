namespace FitnessApp.Shared.Kernel.Contracts;

public interface IIdentityModuleApi
{
    Task <bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default);
}