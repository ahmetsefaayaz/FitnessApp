using FitnessApp.Modules.Workout.Entities;
using MediatR;

namespace FitnessApp.Modules.Workout.Features.Routines.Update;

public record UpdateRoutineCommand(Guid Id, string Name): IRequest;