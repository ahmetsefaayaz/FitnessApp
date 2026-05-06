using FitnessApp.Modules.Nutrition.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessApp.Modules.Nutrition.Infrastructure.Configurations;

public class DietConfiguration: IEntityTypeConfiguration<Diet>
{
    public void Configure(EntityTypeBuilder<Diet> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(f => f.UserId).IsRequired();
        builder.Property(f => f.Title).IsRequired().HasMaxLength(50);
        
    }
}