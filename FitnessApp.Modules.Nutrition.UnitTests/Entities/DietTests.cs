using FitnessApp.Modules.Nutrition.Entities;
using FitnessApp.Shared.Kernel.Exceptions;
using FluentAssertions;

namespace FitnessApp.Modules.Nutrition.UnitTests.Entities;

public class DietTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldCreateDiet()
    {
        var userId = Guid.NewGuid();
        var title = "Yaz definisyonu diyeti";
        var diet = Diet.Create(userId, title);
        diet.Should().NotBeNull();
        diet.UserId.Should().Be(userId);
        diet.Title.Should().Be(title);
        diet.Foods.Should().BeEmpty();
    }

    [Fact]
    public void Create_WithInvalidParameters_ShouldThrowException()
    {
        var userId = Guid.NewGuid();
        string title = null;
        Action act = () => Diet.Create(userId, title);
        act.Should().Throw<WrongInputException>()
            .WithMessage($"{nameof(title)} cannot be null or empty");
    }
    
    [Fact]
    public void AddFood_WithNegativeAmount_ShouldThrowException()
    {
        var diet = Diet.Create(Guid.NewGuid(), "Standart diyet");
        var foodId =  Guid.NewGuid();
        var invalidAmount = -50.0;
        Action act = () => diet.AddFood(foodId, invalidAmount);
        act.Should().Throw<WrongInputException>()
            .WithMessage("*Miktar 0'dan büyük olmalıdır!*");
    }

    [Fact]
    public void AddFood_WithExistingFood_ShouldThrowException()
    {
        var id =  Guid.NewGuid();
        var diet = Diet.Create(id, "diyet");
        var food1 = diet.AddFood(id, 5);
        Action act = () => diet.AddFood(id, 8);
        act.Should().Throw<WrongInputException>()
            .WithMessage($"Food with id {id} already exists");

    }

    [Fact]
    public void UpdateFoodAmount_WithValidParameters_ShouldUpdateFoodAmount()
    {
        var diet = Diet.Create(Guid.NewGuid(), "Standart diyet");
        var foodId =  Guid.NewGuid();
        var food =  diet.AddFood(foodId, 5);
        var validAmount = 50.0;
        Action act =() => diet.UpdateFoodAmount(foodId, validAmount);
        act.Should().NotThrow();
        
    }

    [Fact]
    public void UpdateFoodAmount_WithNegativeAmount_ShouldThrowException()
    {
        var diet = Diet.Create(Guid.NewGuid(), "Standart diyet");
        var foodId =  Guid.NewGuid();
        var food =  diet.AddFood(foodId, 5);
        var invalidAmount = -50.0;
        Action act =() => diet.UpdateFoodAmount(foodId, invalidAmount);
        act.Should().Throw<WrongInputException>();
    }

    [Fact]
    public void UpdateFoodAmount_WithNonExistingFood_ShouldThrowException()
    {
        var diet = Diet.Create(Guid.NewGuid(), "Standart diyet");
        var foodId =  Guid.NewGuid();
        Action act = () => diet.UpdateFoodAmount(foodId, 5);
        act.Should().Throw<NotFoundException>();
    }
    
    
}