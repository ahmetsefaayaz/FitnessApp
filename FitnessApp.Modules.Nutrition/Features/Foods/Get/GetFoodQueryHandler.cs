using FitnessApp.Modules.Nutrition.Entities;
using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Get;

internal sealed class GetFoodQueryHandler: IRequestHandler<GetFoodQuery, Food>
{
    private readonly NutritionDbContext _dbContext;

    public GetFoodQueryHandler(NutritionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Food> Handle(GetFoodQuery request, CancellationToken cancellationToken)
    {
        var food = await _dbContext.Foods.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        if (food == null) throw new NotFoundException("Food not found");
        return food;
    }
}