using FitnessApp.Modules.Nutrition.Entities;
using FitnessApp.Modules.Nutrition.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Get;

internal sealed class GetFoodsQueryHandler: IRequestHandler<GetFoodsQuery, List<Food>>
{
    private readonly NutritionDbContext _dbContext;

    public GetFoodsQueryHandler(NutritionDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<List<Food>> Handle(GetFoodsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Foods.ToListAsync(cancellationToken);
    }
}