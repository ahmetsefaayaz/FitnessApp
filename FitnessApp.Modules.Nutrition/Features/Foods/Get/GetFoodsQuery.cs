using FitnessApp.Modules.Nutrition.Entities;
using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Get;

public record GetFoodsQuery: IRequest<List<Food>>;