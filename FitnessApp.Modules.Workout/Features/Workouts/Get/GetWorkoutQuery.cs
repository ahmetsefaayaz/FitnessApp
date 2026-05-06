using MediatR;

namespace FitnessApp.Modules.Workout.Features.Workouts.Get;

public record GetWorkoutQuery(Guid WorkoutId): IRequest<GetWorkoutResponse>;
public record GetWorkoutResponse(Guid Id, string Name, string Description);