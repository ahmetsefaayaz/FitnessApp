using MediatR;

namespace FitnessApp.Modules.Workout.Features.Workouts.Create;

public record CreateWorkoutCommand(string Name, string Description): IRequest<Guid>;