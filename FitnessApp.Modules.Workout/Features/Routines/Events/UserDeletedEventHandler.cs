using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Features.Routines.Events;

internal sealed class UserDeletedEventHandler: INotificationHandler<UserDeletedEvent>
{
    private readonly WorkoutDbContext _context;
    public UserDeletedEventHandler(WorkoutDbContext context)
    {
        _context = context;
    }
    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        var userId = notification.UserId;
        var userRoutines = await _context.Routines
            .Where(r => r.UserId == userId)
            .ToListAsync(cancellationToken);
        if (userRoutines.Any())
        {
            _context.Routines.RemoveRange(userRoutines);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}