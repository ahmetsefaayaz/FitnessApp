using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessApp.Modules.Workout.Infrastructure.Configurations;

internal sealed class WorkoutConfiguration: IEntityTypeConfiguration<Entities.Workout>
{
    public void Configure(EntityTypeBuilder<Entities.Workout> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
        
    }
}