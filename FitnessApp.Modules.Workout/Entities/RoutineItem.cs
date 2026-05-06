using FitnessApp.Shared.Kernel.Entities;

namespace FitnessApp.Modules.Workout.Entities;

public sealed class RoutineItem: BaseEntity
{
    public Guid RoutineId { get; private set; }
    public Guid WorkoutId { get; private set; }
    public int Sets { get; private set; }
    public int Reps { get; private set; }
    private RoutineItem() {}
    internal static RoutineItem Create(Guid routineId, Guid workoutId, int sets, int reps)
    {
        return new RoutineItem
        {
            RoutineId = routineId,
            WorkoutId = workoutId,
            Sets = sets,
            Reps = reps
        };
    }

}