using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Create;

public record CreateFoodCommand(string Name, string Description, double Calories, double Carbs, double Protein, double Fats): IRequest<Guid>;