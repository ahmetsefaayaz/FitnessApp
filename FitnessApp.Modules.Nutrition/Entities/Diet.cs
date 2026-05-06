using FitnessApp.Shared.Kernel.Entities;
using FitnessApp.Shared.Kernel.Exceptions;

namespace FitnessApp.Modules.Nutrition.Entities;

public sealed class Diet: BaseEntity
{
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    private readonly List<FoodItem> _foods = new();
    public IReadOnlyCollection<FoodItem> Foods => _foods.AsReadOnly();

    public static Diet Create(Guid userId, string title)
    {
        if (string.IsNullOrEmpty(title)) throw new WrongInputException($"{nameof(title)} cannot be null or empty");
        return new Diet
        {
            UserId = userId,
            Title = title
        };
    }

    public FoodItem AddFood(Guid foodId,  double amount)
    {
        if (amount <= 0) throw new WrongInputException($"Miktar 0'dan büyük olmalıdır!");
        if(_foods.Any(x => x.FoodId == foodId)) throw new WrongInputException($"Food with id {foodId} already exists");
        var newFood = FoodItem.Create(Id, foodId, amount);
        _foods.Add(newFood);
        return newFood;
    }

    public void UpdateFoodAmount(Guid foodId, double amount)
    {
        if (amount <= 0) throw new WrongInputException("Miktar 0'dan büyük olmalıdır!");
        if(!_foods.Any(f => f.FoodId ==  foodId)) throw new NotFoundException($"Food with id {foodId} does not exist");
        var food = _foods.First(f => f.FoodId == foodId);
        food.UpdateAmount(amount);
    }

    public void UpdateTitle(string title)
    {
        
        if(string.IsNullOrEmpty(title)) throw new WrongInputException("Title is required");
        Title = title;
    }
    
}