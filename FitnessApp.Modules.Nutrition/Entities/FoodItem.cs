using FitnessApp.Shared.Kernel.Entities;
using FitnessApp.Shared.Kernel.Exceptions;

namespace FitnessApp.Modules.Nutrition.Entities;

public sealed class FoodItem: BaseEntity
{
    public Guid DietId { get; private set; }
    public Guid FoodId { get; private set; }
    public double Amount { get; private set; }
    
    
    public static FoodItem Create(Guid dietId, Guid foodId, double amount)
    {
        return new FoodItem
        {
            DietId =  dietId,
            FoodId = foodId,
            Amount = amount
        };
    }
    public void UpdateAmount(double amount)
    {
        if (amount < 0) throw new WrongInputException($"{nameof(amount)}  cannot be negative");
        Amount = amount;
    }
    
    
}