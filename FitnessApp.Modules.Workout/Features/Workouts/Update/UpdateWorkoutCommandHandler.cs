using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Features.Workouts.Update;

internal sealed class UpdateWorkoutCommandHandler: IRequestHandler<UpdateWorkoutCommand>
{
    private readonly WorkoutDbContext _workoutDbContext;

    public UpdateWorkoutCommandHandler(WorkoutDbContext workoutDbContext)
    {
        _workoutDbContext = workoutDbContext;
    }
    public async Task Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _workoutDbContext.Workouts.FirstOrDefaultAsync(w => w.Id == request.WorkoutId, cancellationToken);
        if (workout == null) throw new NotFoundException("Workout not found");
        workout.Update(request.Name, request.Description);
        await _workoutDbContext.SaveChangesAsync(cancellationToken);
    }
}