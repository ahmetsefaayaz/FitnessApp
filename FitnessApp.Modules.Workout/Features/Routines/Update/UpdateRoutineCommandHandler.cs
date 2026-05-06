using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Features.Routines.Update;

public class UpdateRoutineCommandHandler: IRequestHandler<UpdateRoutineCommand>
{
    private readonly WorkoutDbContext _context;
    public UpdateRoutineCommandHandler(WorkoutDbContext context)
    {
        _context = context;
    }
    public async Task Handle(UpdateRoutineCommand request, CancellationToken cancellationToken)
    {
        var routine = await _context.Routines.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        if (routine == null) throw new NotFoundException($"Routine {request.Id} does not exist");
        routine.UpdateName(request.Name);
        await _context.SaveChangesAsync(cancellationToken);
    }
}