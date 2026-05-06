using FitnessApp.Modules.Workout.Entities;
using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Features.Routines.AddWorkout;

internal sealed class AddWorkoutToRoutineCommandHandler: IRequestHandler<AddWorkoutToRoutineCommand>
{
    private readonly WorkoutDbContext _workoutDbContext;

    public AddWorkoutToRoutineCommandHandler(WorkoutDbContext workoutDbContext)
    {
        _workoutDbContext = workoutDbContext;
    }
    public async Task Handle(AddWorkoutToRoutineCommand request, CancellationToken cancellationToken)
    {
        var workout = await _workoutDbContext.Workouts
            .FirstOrDefaultAsync(w => w.Id == request.WorkoutId, cancellationToken);
        if (workout == null) throw new NotFoundException("Workout not found");
        
        var routine = await _workoutDbContext.Routines
            .Include(r => r.RoutineItems)
            .AsTracking()
            .FirstOrDefaultAsync(r => r.Id == request.RoutineId, cancellationToken);
        if (routine == null) throw new NotFoundException("Routine not found");
        
        routine.AddWorkout(workout.Id);
        await _workoutDbContext.SaveChangesAsync(cancellationToken);
    }
}