using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Restore;

public record RestoreFoodCommand(Guid Id): IRequest;