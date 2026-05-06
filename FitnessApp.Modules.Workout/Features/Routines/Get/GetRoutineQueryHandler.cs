using System.Text.Json;
using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace FitnessApp.Modules.Workout.Features.Routines.Get;

internal sealed class GetRoutineQueryHandler: IRequestHandler<GetRoutineQuery, GetRoutineResponse>
{
    private readonly WorkoutDbContext _context;
    private readonly IDistributedCache _cache;
    public GetRoutineQueryHandler(WorkoutDbContext context,  IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }
    public async Task<GetRoutineResponse> Handle(GetRoutineQuery request,
        CancellationToken cancellationToken)
    {
        string cacheKey = $"Routine_{request.Id}";
        var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<GetRoutineResponse>(cachedData);
        }

        var routine = await _context
            .Routines
            .Include(r => r.RoutineItems)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        if (routine == null) throw new NotFoundException("Routine not found");
        var response = new GetRoutineResponse(routine.Id, routine.UserId, routine.Name, routine.RoutineItems);
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };
        var serializedResponse = JsonSerializer.Serialize(response);
        await _cache.SetStringAsync(cacheKey, serializedResponse, cacheOptions, cancellationToken);
        return response;

    }
}