using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Update;

internal sealed class UpdateFoodAmountCommandHandler: IRequestHandler<UpdateFoodAmountCommand>
{
    private readonly NutritionDbContext _dbContext;

    public UpdateFoodAmountCommandHandler(NutritionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(UpdateFoodAmountCommand request, CancellationToken cancellationToken)
    {
        var diet = await _dbContext.Diets
            .Include(fi => fi.Foods)
            .FirstOrDefaultAsync(x => x.Id == request.DietId,  cancellationToken);
        if (diet is null) throw new NotFoundException("Diet not found");
        var foodItem = diet.Foods.First(x => x.Id == request.FoodItemId);
        if(foodItem is null) throw new NotFoundException("Food not found");
        
        diet.UpdateFoodAmount(foodItem.FoodId, request.Amount);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}