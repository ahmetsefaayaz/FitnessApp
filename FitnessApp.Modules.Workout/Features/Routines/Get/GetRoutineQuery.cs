using FitnessApp.Modules.Workout.Entities;
using MediatR;

namespace FitnessApp.Modules.Workout.Features.Routines.Get;

public record GetRoutineQuery(Guid Id): IRequest<GetRoutineResponse>;
public record GetRoutineResponse(Guid Id, Guid UserId, string Name, IReadOnlyCollection<RoutineItem> RoutineItems);