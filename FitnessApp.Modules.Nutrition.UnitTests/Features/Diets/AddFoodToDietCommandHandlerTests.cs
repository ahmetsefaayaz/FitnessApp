using FitnessApp.Modules.Nutrition.Entities;
using FitnessApp.Modules.Nutrition.Features.Diets.AddFood;
using FitnessApp.Modules.Nutrition.Infrastructure;
using FitnessApp.Shared.Kernel.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.UnitTests.Features.Diets;

public class AddFoodToDietCommandHandlerTests
{
    private NutritionDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<NutritionDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new NutritionDbContext(options);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldAddFoodAndSaveChanges()
    {
        var context = CreateInMemoryDbContext();
        var userId = Guid.NewGuid();
        var diet = Diet.Create(userId, "Diyet");
        var food = Food.Create("Elma", "Tatlı bir meyve", 1, 1, 1, 1);
        context.Foods.Add(food);
        context.Diets.Add(diet);
        await context.SaveChangesAsync();
        var handler = new AddFoodToDietCommandHandler(context);
        var command = new AddFoodToDietCommand(diet.Id, food.Id, 25);
        await handler.Handle(command, CancellationToken.None);
        var updatedDiet = await context.Diets
            .Include(d => d.Foods)
            .FirstOrDefaultAsync(d => d.Id == diet.Id);
        updatedDiet.Should().NotBeNull();
        updatedDiet!.Foods.Should().HaveCount(1);
        var addedFood = updatedDiet.Foods.Single();
        addedFood.FoodId.Should().Be(food.Id);
        addedFood.Amount.Should().Be(25);
    }

    [Fact]
    public async Task Handle_WithNonExistingFood_ShouldThrowException()
    {
        var context = CreateInMemoryDbContext();
        var userId = Guid.NewGuid();
        var diet = Diet.Create(userId, "Diyet");
        var nonExistingFoodId = Guid.NewGuid();
        context.Diets.Add(diet);
        var handler = new AddFoodToDietCommandHandler(context);
        var command = new AddFoodToDietCommand(diet.Id, nonExistingFoodId, 25);
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<NotFoundException>();
    }
    [Fact]
    public async Task Handle_WithNonExistingDiet_ShouldThrowException()
    {
        var context = CreateInMemoryDbContext();
        var food = Food.Create("Elma", "Tatlı bir meyve", 1, 1, 1, 1);  
        var nonExistingDietId = Guid.NewGuid();
        context.Foods.Add(food);
        var handler = new AddFoodToDietCommandHandler(context);
        var command = new AddFoodToDietCommand(nonExistingDietId, food.Id,25);
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<NotFoundException>();
    }
    [Fact]
    public async Task Handle_WithAlreadyAddedFood_ShouldThrowException()
    {
        var context = CreateInMemoryDbContext();
        var userId = Guid.NewGuid();
        var diet = Diet.Create(userId, "Diyet");
        var food = Food.Create("Elma", "Tatlı bir meyve", 1, 1, 1, 1);
        diet.AddFood(food.Id, 20);
        context.Foods.Add(food);
        context.Diets.Add(diet);
        await context.SaveChangesAsync();
        var handler = new AddFoodToDietCommandHandler(context);
        var command = new AddFoodToDietCommand(diet.Id, food.Id, 25);
        Func<Task> act = async ()=> await handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<WrongInputException>();
    }
    
    
}