using FitnessApp.Modules.Workout.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Workout.Infrastructure;

public sealed class WorkoutDbContext: DbContext
{
    public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options) {}
    
    public DbSet<Routine> Routines { get; set; }
    public DbSet<Entities.Workout>  Workouts { get; set; }
    public DbSet<RoutineItem> RoutineItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkoutDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}