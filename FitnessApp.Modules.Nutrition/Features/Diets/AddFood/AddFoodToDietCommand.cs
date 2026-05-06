using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Diets.AddFood;

public record AddFoodToDietCommand(Guid DietId, Guid FoodId, double Amount): IRequest<Guid>;