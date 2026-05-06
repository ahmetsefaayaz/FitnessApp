using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Update;

public record UpdateFoodAmountCommand(Guid DietId, Guid FoodItemId, double Amount): IRequest;