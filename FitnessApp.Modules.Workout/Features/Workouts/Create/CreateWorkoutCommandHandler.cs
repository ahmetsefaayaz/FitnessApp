using FitnessApp.Modules.Workout.Infrastructure;
using MediatR;

namespace FitnessApp.Modules.Workout.Features.Workouts.Create;

public sealed class CreateWorkoutCommandHandler: IRequestHandler<CreateWorkoutCommand, Guid>
{
    private readonly WorkoutDbContext _workoutDbContext;

    public CreateWorkoutCommandHandler(WorkoutDbContext workoutDbContext)
    {
        _workoutDbContext = workoutDbContext;
    }
    
    public async Task<Guid> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        Entities.Workout workout = Entities.Workout.Create(request.Name, request.Description);
        await _workoutDbContext.AddAsync(workout,  cancellationToken);
        await _workoutDbContext.SaveChangesAsync(cancellationToken);
        return workout.Id;
    }
}