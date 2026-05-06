using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Create;

public record CreateDietCommand(Guid UserId, string Title): IRequest<Guid>;