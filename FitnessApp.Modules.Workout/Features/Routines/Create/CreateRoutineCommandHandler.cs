using FitnessApp.Modules.Workout.Entities;
using FitnessApp.Modules.Workout.Infrastructure;
using FitnessApp.Shared.Kernel.Contracts;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;

namespace FitnessApp.Modules.Workout.Features.Routines.Create;

internal sealed class CreateRoutineCommandHandler: IRequestHandler<CreateRoutineCommand, Guid>
{
    private readonly WorkoutDbContext _workoutDbContext;
    private readonly IIdentityModuleApi _identityModuleApi;

    public CreateRoutineCommandHandler(WorkoutDbContext workoutDbContext,  IIdentityModuleApi identityModuleApi)
    {
        _workoutDbContext = workoutDbContext;
        _identityModuleApi = identityModuleApi;
    }
    
    public async Task<Guid> Handle(CreateRoutineCommand request, CancellationToken cancellationToken)
    {
        if(! await _identityModuleApi.UserExistsAsync(request.UserId)) throw new NotFoundException("User not found");
        
        var routine = Routine.Create(request.UserId, request.Name);
        await _workoutDbContext.Routines.AddAsync(routine, cancellationToken);
        await _workoutDbContext.SaveChangesAsync(cancellationToken);
        return routine.Id;
    }
}