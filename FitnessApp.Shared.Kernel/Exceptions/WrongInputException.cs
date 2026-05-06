namespace FitnessApp.Shared.Kernel.Exceptions;

public class WrongInputException: Exception
{
    public WrongInputException(string message) : base(message) {}
}