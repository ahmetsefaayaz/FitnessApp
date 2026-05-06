using System.Text.Json;
using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace FitnessApp.Modules.Workout.Features.Workouts.Get;

internal sealed class GetWorkoutQueryHandler: IRequestHandler<GetWorkoutQuery, GetWorkoutResponse>
{
    private readonly WorkoutDbContext _context;
    private readonly IDistributedCache _cache;

    public GetWorkoutQueryHandler(IDistributedCache cache,  WorkoutDbContext context)
    {
        _cache = cache;
        _context = context;
    }
    public async Task<GetWorkoutResponse> Handle(GetWorkoutQuery request, CancellationToken cancellationToken)
    {
        string key = $"Workouts_{request.WorkoutId}";
        var cachedData = await _cache.GetStringAsync(key, cancellationToken);
        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<GetWorkoutResponse>(cachedData);
        }
        var workout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == request.WorkoutId, cancellationToken);
        if (workout == null) throw new NotFoundException("Workout not found");
        var response = new GetWorkoutResponse(workout.Id, workout.Name, workout.Description);
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
        var serializedResponse = JsonSerializer.Serialize(response);
        await _cache.SetStringAsync(key, serializedResponse, cacheOptions, cancellationToken);
        return response;

    }
}