using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Restore;

internal class RestoreDietCommandHandler: IRequestHandler<RestoreDietCommand>
{
    private readonly NutritionDbContext _context;
    public RestoreDietCommandHandler(NutritionDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(RestoreDietCommand request, CancellationToken cancellationToken)
    {
        var  diet = await _context.Diets.FirstOrDefaultAsync(x => x.Id == request.DietId, cancellationToken);
        if (diet is null) throw new NotFoundException("Diet not found");
        diet.Restore();
        await _context.SaveChangesAsync(cancellationToken);
    }
}