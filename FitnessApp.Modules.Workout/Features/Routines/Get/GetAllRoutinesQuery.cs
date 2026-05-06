using FitnessApp.Modules.Workout.Entities;
using MediatR;

namespace FitnessApp.Modules.Workout.Features.Routines.Get;

public record GetAllRoutinesQuery(): IRequest<List<Routine>>;