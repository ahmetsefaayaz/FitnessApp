using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Delete;

public record DeleteFoodCommand(Guid Id, bool Permanent): IRequest;