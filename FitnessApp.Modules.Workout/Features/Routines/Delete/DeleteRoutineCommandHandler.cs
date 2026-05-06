using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Contracts;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Features.Routines.Delete;

internal sealed class DeleteRoutineCommandHandler: IRequestHandler<DeleteRoutineCommand>
{
    private readonly WorkoutDbContext _context;
    private readonly IUserRolesAndClaims _userRolesAndClaims;

    public DeleteRoutineCommandHandler(WorkoutDbContext context, IUserRolesAndClaims userRolesAndClaims)
    {
        _context = context;
        _userRolesAndClaims = userRolesAndClaims;
    }
    public async Task Handle(DeleteRoutineCommand request, CancellationToken cancellationToken)
    {
        var routine = await _context.Routines.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        if (routine == null) throw new NotFoundException("RoutineNotFound");
        if (request.Permanent)
        {
            if (!_userRolesAndClaims.HasRole("Admin"))
                throw new UnauthorizedAccessException("You must be an Admin to hard delete a routine.");
            _context.Routines.Remove(routine);
        }
        else
        {
            routine.SoftDelete();
        }
        await _context.SaveChangesAsync(cancellationToken);
    }
}