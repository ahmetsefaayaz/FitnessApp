using FitnessApp.Shared.Kernel.Exceptions;

namespace FitnessApp.Shared.Kernel.Entities;

public class BaseEntity
{
    public Guid Id { get; private set; } =  Guid.NewGuid();
    public DateTime CreatedOn { get; private set; } =  DateTime.UtcNow;
    public bool IsDeleted { get; private set; } = false;

    public void SoftDelete()
    {
        if (IsDeleted) throw new WrongInputException($"{GetType().Name} is already deleted");
        IsDeleted = true;
    }
    public void Restore()
    {
        if (!IsDeleted) throw new WrongInputException($"{GetType().Name} is not deleted");
        IsDeleted = false;
    }
}