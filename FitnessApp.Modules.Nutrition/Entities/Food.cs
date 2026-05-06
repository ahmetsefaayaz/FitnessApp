using FitnessApp.Shared.Kernel.Entities;
using FitnessApp.Shared.Kernel.Exceptions;

namespace FitnessApp.Modules.Nutrition.Entities;

public sealed class Food : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Calories { get; private set; }
    public double Protein { get; private set; }
    public double Carbohydrates { get; private set; }
    public double Fats { get; private set; }
    

    public static Food Create(string name, string description, double calories, double protein, double carbs, double fats)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new WrongInputException("Besin adı boş olamaz.");

        if (calories < 0 || protein < 0 || carbs < 0 || fats < 0)
            throw new WrongInputException("Makro değerleri veya kalori 0'dan küçük olamaz.");

        return new Food
        {
            Description = description,
            Calories = calories,
            Carbohydrates = carbs,
            Fats = fats,
            Name = name,
            Protein = protein
        };
    }

    public void UpdateMacros(double newCalories, double newProtein, double newCarbs, double newFats)
    {
        if (newCalories < 0 || newProtein < 0 || newCarbs < 0 || newFats < 0)
            throw new WrongInputException("Makro değerleri negatif olamaz.");

        Calories = newCalories;
        Protein = newProtein;
        Carbohydrates = newCarbs;
        Fats = newFats;
    }
    
}