using FitnessApp.Modules.Nutrition.Entities;
using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Get;

internal sealed class GetDietQueryHandler: IRequestHandler<GetDietQuery, Diet>
{
    private readonly NutritionDbContext _dbContext;

    public GetDietQueryHandler(NutritionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Diet> Handle(GetDietQuery request, CancellationToken cancellationToken)
    {
        var diet = await _dbContext.Diets
            .Include(d => d.Foods)
            .FirstOrDefaultAsync(d => d.Id == request.DietId);
        if (diet == null) throw new NotFoundException("Diet not found");
        return diet;
    }
}