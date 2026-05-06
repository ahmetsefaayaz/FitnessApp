using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Contracts;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Delete;

internal sealed class DeleteDietCommandHandler:IRequestHandler<DeleteDietCommand>
{
    private readonly NutritionDbContext _dbContext;
    private readonly IUserRolesAndClaims _userRolesAndClaims;

    public DeleteDietCommandHandler(NutritionDbContext dbContext, IUserRolesAndClaims userRolesAndClaims)
    {
        _dbContext = dbContext;
        _userRolesAndClaims = userRolesAndClaims;
    }
    public async Task Handle(DeleteDietCommand request, CancellationToken cancellationToken)
    {
        var diet = await _dbContext.Diets.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
        if (diet == null) throw new NotFoundException("Diet not found");
        
        if (request.Permanent)
        {
            if (!_userRolesAndClaims.HasRole("Admin"))
                throw new UnauthorizedAccessException("You must be an Admin to hard delete a diet.");
            _dbContext.Diets.Remove(diet);
        }
        else
        {
            diet.SoftDelete();
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}