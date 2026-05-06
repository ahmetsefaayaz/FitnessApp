using FitnessApp.Modules.Nutrition.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessApp.Modules.Nutrition.Infrastructure.Configurations;

public class FoodItemConfiguration: IEntityTypeConfiguration<FoodItem>
{
    public void Configure(EntityTypeBuilder<FoodItem> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedNever();
        builder.Property(f => f.DietId).IsRequired();
        builder.Property(f => f.FoodId).IsRequired();
    }
}