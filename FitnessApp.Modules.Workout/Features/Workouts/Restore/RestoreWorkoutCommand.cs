using MediatR;

namespace FitnessApp.Modules.Workout.Features.Workouts.Restore;

public record RestoreWorkoutCommand(Guid WorkoutId) : IRequest;