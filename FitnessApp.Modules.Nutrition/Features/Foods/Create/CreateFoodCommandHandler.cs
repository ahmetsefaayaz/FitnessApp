using FitnessApp.Modules.Nutrition.Entities;
using FitnessApp.Modules.Nutrition.Infrastructure;
using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Create;

internal sealed class CreateFoodCommandHandler: IRequestHandler<CreateFoodCommand, Guid>
{
    private readonly NutritionDbContext _context;
    public CreateFoodCommandHandler(NutritionDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
    {
        var newFood = Food.Create(request.Name, request.Description, request.Calories, request.Protein, request.Carbs,
            request.Fats);
        await _context.Foods.AddAsync(newFood, cancellationToken);
        await  _context.SaveChangesAsync(cancellationToken);
        return newFood.Id;
    }
}