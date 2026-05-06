using FitnessApp.Modules.Workout.Entities;
using FitnessApp.Modules.Workout.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Features.Routines.Get;

internal sealed class GetAllRoutinesQueryHandler: IRequestHandler<GetAllRoutinesQuery, List<Routine>>
{
    private readonly WorkoutDbContext _dbContext;

    public GetAllRoutinesQueryHandler(WorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Routine>> Handle(GetAllRoutinesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Routines.ToListAsync(cancellationToken);
    }
}