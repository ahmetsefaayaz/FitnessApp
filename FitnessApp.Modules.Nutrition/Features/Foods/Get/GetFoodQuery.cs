using FitnessApp.Modules.Nutrition.Entities;
using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Get;

public record GetFoodQuery(Guid Id): IRequest<Food>;