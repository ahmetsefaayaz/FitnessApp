using FitnessApp.Modules.Nutrition.Entities;
using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Get;

public record GetDietsQuery: IRequest<List<Diet>>;