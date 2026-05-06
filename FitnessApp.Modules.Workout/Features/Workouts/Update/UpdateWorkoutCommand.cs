using MediatR;

namespace FitnessApp.Modules.Workout.Features.Workouts.Update;

public record UpdateWorkoutCommand(Guid WorkoutId, string Name, string Description): IRequest;