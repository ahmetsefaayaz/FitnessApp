using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Restore;

internal sealed class RestoreFoodCommandHandler: IRequestHandler<RestoreFoodCommand>
{
    private readonly NutritionDbContext _dbContext;

    public RestoreFoodCommandHandler(NutritionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(RestoreFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _dbContext.Foods.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        if (food is null) throw new NotFoundException("Food not found");
        food.Restore();
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}