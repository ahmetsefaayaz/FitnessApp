using FitnessApp.Modules.Nutrition.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessApp.Modules.Nutrition.Infrastructure.Configurations;

public class FoodConfiguration: IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(f => f.Name).IsRequired().HasMaxLength(50);
        builder.Property(f => f.Description).IsRequired().HasMaxLength(200);
    }
}