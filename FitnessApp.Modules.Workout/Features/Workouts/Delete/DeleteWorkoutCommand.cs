using MediatR;

namespace FitnessApp.Modules.Workout.Features.Workouts.Delete;

public record DeleteWorkoutCommand(Guid WorkoutId, bool Permanent): IRequest;