using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Update;

internal sealed class UpdateFoodMacrosCommandHandler: IRequestHandler<UpdateFoodMacrosCommand>
{
    private readonly NutritionDbContext _context;
    public UpdateFoodMacrosCommandHandler(NutritionDbContext context)
    {
        _context = context;
    }
    public async Task Handle(UpdateFoodMacrosCommand request, CancellationToken cancellationToken)
    {
        var food = await _context.Foods.FirstOrDefaultAsync(f => f.Id == request.FoodId,  cancellationToken);
        if (food == null) throw new NotFoundException("Food not found");
        
        food.UpdateMacros(request.NewCalories, request.NewProtein, request.NewCarbs,  request.NewFats);
        await _context.SaveChangesAsync(cancellationToken);
    }
}