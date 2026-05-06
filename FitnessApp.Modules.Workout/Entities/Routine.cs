using FitnessApp.Shared.Kernel.Entities;
using FitnessApp.Shared.Kernel.Exceptions;

namespace FitnessApp.Modules.Workout.Entities;

public sealed class Routine : BaseEntity
{
    public Guid UserId { get; private set; }
    public string? Name { get; private set; }
    
    private readonly List<RoutineItem> _routineItems = new();
    public IReadOnlyCollection<RoutineItem> RoutineItems => _routineItems.AsReadOnly();

    public static Routine Create(Guid userId, string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new WrongInputException("Rutin Adı boş olamaz!");
        return new Routine
        {
            UserId = userId,
            Name = name
        };
    }
    
    public void AddWorkout(Guid workoutId, int sets = 3, int reps = 10)
    {
        if (_routineItems.Count >= 10)
            throw new WrongInputException($"Bir rutine {_routineItems.Count}'dan fazla antrenman eklenemez.");
            
        if (_routineItems.Any(i => i.WorkoutId == workoutId))
            throw new WrongInputException("Bu antrenman rutinde zaten mevcut.");

        var newItem = RoutineItem.Create(this.Id, workoutId, sets, reps);
        
        _routineItems.Add(newItem);
    }

    public void UpdateName(string name) => this.Name = name;
}