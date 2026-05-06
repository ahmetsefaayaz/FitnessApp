namespace FitnessApp.Shared.Kernel.Exceptions;

public class NotFoundException: Exception
{
    public NotFoundException(string message) : base(message) {}
}