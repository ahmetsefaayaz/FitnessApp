using FitnessApp.Modules.Workout.Entities;
using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Contracts;
using MediatR;

namespace FitnessApp.Modules.Workout.Features.Routines.Events;

internal sealed class UserCreatedEventHandler: INotificationHandler<UserCreatedEvent>
{
    private readonly WorkoutDbContext _dbContext;

    public UserCreatedEventHandler(WorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var baseName = "Varsayilan_Baslik";
        var routine = Routine.Create(notification.UserId, baseName);
        await _dbContext.Routines.AddAsync(routine, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}