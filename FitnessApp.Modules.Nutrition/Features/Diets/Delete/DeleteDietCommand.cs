using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Delete;

public record DeleteDietCommand(Guid Id, bool Permanent): IRequest;