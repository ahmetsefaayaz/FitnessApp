using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Contracts;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Features.Workouts.Delete;

internal sealed class DeleteWorkoutCommandHandler:IRequestHandler<DeleteWorkoutCommand>
{
    private readonly WorkoutDbContext _dbContext;
    private readonly IUserRolesAndClaims _userRolesAndClaims;

    public DeleteWorkoutCommandHandler(WorkoutDbContext dbContext,  IUserRolesAndClaims userRolesAndClaims)
    {
        _dbContext = dbContext;
        _userRolesAndClaims = userRolesAndClaims;
    }
    public async Task Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _dbContext.Workouts.FirstOrDefaultAsync(w => w.Id == request.WorkoutId,cancellationToken);
        if (workout is null) throw new NotFoundException("Workout not found");
        if (request.Permanent)
        {
            if (!_userRolesAndClaims.HasRole("Admin"))
                throw new UnauthorizedAccessException("You must be an Admin to hard delete a workout.");
            _dbContext.Workouts.Remove(workout);
        }
        else
        {
            workout.SoftDelete();
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}