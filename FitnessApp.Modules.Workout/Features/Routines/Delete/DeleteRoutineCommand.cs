using MediatR;

namespace FitnessApp.Modules.Workout.Features.Routines.Delete;

public record DeleteRoutineCommand(Guid Id, bool Permanent): IRequest;