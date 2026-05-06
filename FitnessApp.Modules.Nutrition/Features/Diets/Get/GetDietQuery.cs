using FitnessApp.Modules.Nutrition.Entities;
using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Get;

public record GetDietQuery(Guid DietId): IRequest<Diet>;