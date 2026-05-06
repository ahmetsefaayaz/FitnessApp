using FitnessApp.Modules.Nutrition.Entities;
using FitnessApp.Modules.Nutrition.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Get;

internal sealed class GetDietsQueryHandler: IRequestHandler<GetDietsQuery, List<Diet>>
{
    private readonly NutritionDbContext _context;
    public GetDietsQueryHandler(NutritionDbContext context)
    {
        _context = context;
    }
    public async Task<List<Diet>> Handle(GetDietsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Diets.ToListAsync(cancellationToken);
    }
}