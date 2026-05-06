using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Contracts;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Features.Foods.Delete;

internal sealed class DeleteFoodCommandHandler: IRequestHandler<DeleteFoodCommand>
{
    private readonly NutritionDbContext _dbContext;
    private readonly IUserRolesAndClaims _userRolesAndClaims;

    public DeleteFoodCommandHandler(NutritionDbContext context,  IUserRolesAndClaims userRolesAndClaims)
    {
        _dbContext = context;
        _userRolesAndClaims = userRolesAndClaims;
    }
    public async Task Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _dbContext.Foods.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        if (food is null) throw new NotFoundException("Food not found");
        if (request.Permanent)
        {
            if (!_userRolesAndClaims.HasRole("Admin"))
                throw new UnauthorizedAccessException("You must be an Admin to hard delete a food.");
            _dbContext.Foods.Remove(food);
        }
        else
        {
            food.SoftDelete();
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}