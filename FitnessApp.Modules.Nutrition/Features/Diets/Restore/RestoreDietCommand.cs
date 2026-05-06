using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Restore;

public record RestoreDietCommand(Guid DietId): IRequest;