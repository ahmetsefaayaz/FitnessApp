using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Update;

public record UpdateDietTitleCommand(Guid DietId, string Title): IRequest;