using FitnessApp.Modules.Workout.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Features.Workouts.Get;

public class GetAllWorkoutsQueryHandler: IRequestHandler<GetAllWorkoutsQuery, List<Entities.Workout>>
{
    private readonly WorkoutDbContext _dbContext;

    public GetAllWorkoutsQueryHandler(WorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Entities.Workout>> Handle(GetAllWorkoutsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Workouts.ToListAsync(cancellationToken);
    }
}