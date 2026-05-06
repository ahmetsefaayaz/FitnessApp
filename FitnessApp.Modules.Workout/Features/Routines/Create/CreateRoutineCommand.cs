using MediatR;

namespace FitnessApp.Modules.Workout.Features.Routines.Create;

public record CreateRoutineCommand(Guid UserId, string Name): IRequest<Guid>;