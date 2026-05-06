using MediatR;

namespace FitnessApp.Shared.Kernel.Contracts;

public record UserCreatedEvent(Guid UserId): INotification;