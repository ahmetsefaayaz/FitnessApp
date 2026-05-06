using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Diets.AddFood;

internal sealed class AddFoodToDietCommandHandler: IRequestHandler<AddFoodToDietCommand, Guid>
{
    private readonly NutritionDbContext _dbContext;

    public AddFoodToDietCommandHandler(NutritionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Guid> Handle(AddFoodToDietCommand request, CancellationToken cancellationToken)
    {
        var food = await _dbContext.Foods.FirstOrDefaultAsync(f => f.Id == request.FoodId, cancellationToken);
        if (food == null) throw new NotFoundException("Food not found");
        
        var diet = await _dbContext.Diets.FirstOrDefaultAsync(d => d.Id == request.DietId, cancellationToken);
        if (diet == null) throw new NotFoundException("Diet not found");
        
        var foodItem = diet.AddFood(request.FoodId,  request.Amount);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return foodItem.Id;
    }
}