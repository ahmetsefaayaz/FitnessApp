using FitnessApp.Modules.Nutrition.Entities;
using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Contracts;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;

namespace FitnessApp.Modules.Nutrition.Features.Diets.Create;

internal sealed class CreateDietCommandHandler: IRequestHandler<CreateDietCommand, Guid>
{
    private readonly NutritionDbContext _context;
    private readonly IIdentityModuleApi _api;
    
    
    public CreateDietCommandHandler(NutritionDbContext context,  IIdentityModuleApi api)
    {
        _context = context;
        _api = api;
    }
    
    public async Task<Guid> Handle(CreateDietCommand request, CancellationToken cancellationToken)
    {
        if(!_api.UserExistsAsync(request.UserId, cancellationToken).Result) 
            throw new NotFoundException($"User does not exist");
        var newDiet = Diet.Create(request.UserId, request.Title);
        await _context.AddAsync(newDiet, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return newDiet.Id;
    }
}