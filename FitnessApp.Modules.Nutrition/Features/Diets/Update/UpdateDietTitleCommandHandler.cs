using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Update;

internal sealed class UpdateDietTitleCommandHandler: IRequestHandler<UpdateDietTitleCommand>
{
    private readonly NutritionDbContext _context;
    public UpdateDietTitleCommandHandler(NutritionDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(UpdateDietTitleCommand request, CancellationToken cancellationToken)
    {
        var diet = await _context.Diets.FirstOrDefaultAsync(diet => diet.Id == request.DietId, cancellationToken);
        if(diet is null) throw new NotFoundException("Diet not found");
        diet.UpdateTitle(request.Title);
        await _context.SaveChangesAsync(cancellationToken);
    }
}