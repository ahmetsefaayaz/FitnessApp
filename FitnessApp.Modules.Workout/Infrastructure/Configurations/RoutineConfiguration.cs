using FitnessApp.Modules.Workout.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessApp.Modules.Workout.Infrastructure.Configurations;

internal sealed class RoutineConfiguration:IEntityTypeConfiguration<Routine>
{
    public void Configure(EntityTypeBuilder<Routine> builder)
    {
        //Tabloyu isimlendirmedik, Hata Verebilir... ? - builder.ToTable("Routines", "workout");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Navigation(r => r.RoutineItems)
            .HasField("_routineItems")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(r => r.RoutineItems).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(r => r.RoutineItems)
            .WithOne()
            .HasForeignKey(ri => ri.RoutineId);
    }
}