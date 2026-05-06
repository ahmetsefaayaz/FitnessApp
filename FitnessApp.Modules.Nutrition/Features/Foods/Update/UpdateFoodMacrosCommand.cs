using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Update;

public record UpdateFoodMacrosCommand(Guid FoodId, double NewCalories, double NewProtein, double NewCarbs, double NewFats): IRequest;