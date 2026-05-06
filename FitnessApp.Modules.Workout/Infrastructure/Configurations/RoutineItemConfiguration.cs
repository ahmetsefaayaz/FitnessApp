using FitnessApp.Modules.Workout.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessApp.Modules.Workout.Infrastructure.Configurations;

internal sealed class RoutineItemConfiguration : IEntityTypeConfiguration<RoutineItem>
{
    public void Configure(EntityTypeBuilder<RoutineItem> builder)
    {
        builder.HasKey(ri => ri.Id);
        
        builder.Property(ri => ri.Id).ValueGeneratedNever(); 
    }
}