using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Features.Routines.Restore;

internal sealed class RestoreRoutineCommandHandler: IRequestHandler<RestoreRoutineCommand>
{
    private readonly WorkoutDbContext _context;
    public RestoreRoutineCommandHandler(WorkoutDbContext context)
    {
        _context = context;
    }
    public async Task Handle(RestoreRoutineCommand request, CancellationToken cancellationToken)
    {
        var routine = await _context.Routines.FirstOrDefaultAsync(r => r.Id == request.RoutineId, cancellationToken);
        if (routine is null) throw new NotFoundException("Routine not found");
        routine.Restore();
        await _context.SaveChangesAsync(cancellationToken);
    }
}