using MediatR;

namespace FitnessApp.Modules.Workout.Features.Routines.Restore;

public record RestoreRoutineCommand(Guid RoutineId) : IRequest;