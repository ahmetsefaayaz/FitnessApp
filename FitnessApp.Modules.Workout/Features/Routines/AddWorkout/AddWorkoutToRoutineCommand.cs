using MediatR;

namespace FitnessApp.Modules.Workout.Features.Routines.AddWorkout;

public record AddWorkoutToRoutineCommand(Guid RoutineId, Guid WorkoutId): IRequest;