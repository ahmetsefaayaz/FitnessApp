using FitnessApp.Modules.Nutrition.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Nutrition.Infrastructure;

public class NutritionDbContext: DbContext
{
    public  NutritionDbContext(DbContextOptions<NutritionDbContext> options) : base(options) {}
    public DbSet<Diet> Diets { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<FoodItem> FoodItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NutritionDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}