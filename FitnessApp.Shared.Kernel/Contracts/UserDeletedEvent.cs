using MediatR;

namespace FitnessApp.Shared.Kernel.Contracts;

public record UserDeletedEvent(Guid UserId): INotification;