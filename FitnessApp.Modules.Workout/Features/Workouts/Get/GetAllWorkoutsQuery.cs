using MediatR;

namespace FitnessApp.Modules.Workout.Features.Workouts.Get;

public record GetAllWorkoutsQuery(): IRequest<List<Entities.Workout>>;