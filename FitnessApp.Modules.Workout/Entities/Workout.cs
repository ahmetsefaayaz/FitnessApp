using FitnessApp.Shared.Kernel.Entities;
using FitnessApp.Shared.Kernel.Exceptions;

namespace FitnessApp.Modules.Workout.Entities;

public sealed class Workout: BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public static Workout Create(string name, string description)
    {
        return new Workout
        {
            Name = name,
            Description = description
        };
    }

    public void Update(string name, string description)
    {
        if (string.IsNullOrEmpty(name)) throw new WrongInputException("name cannot be null");
        if (string.IsNullOrEmpty(description)) throw new WrongInputException("description cannot be null");
        Name = name;
        Description = description;
    }
}