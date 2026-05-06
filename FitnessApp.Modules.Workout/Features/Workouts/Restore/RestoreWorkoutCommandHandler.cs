using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Features.Workouts.Restore;

internal sealed class RestoreWorkoutCommandHandler: IRequestHandler<RestoreWorkoutCommand>
{
    private readonly WorkoutDbContext _workoutDbContext;

    public RestoreWorkoutCommandHandler(WorkoutDbContext workoutDbContext)
    {
        _workoutDbContext = workoutDbContext;
    }
    public async Task Handle(RestoreWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _workoutDbContext.Workouts.FirstOrDefaultAsync(w => w.Id == request.WorkoutId,  cancellationToken);
        if (workout is null) throw new NotFoundException("Workout not found");
        workout.Restore();
        await _workoutDbContext.SaveChangesAsync(cancellationToken);
    }
}