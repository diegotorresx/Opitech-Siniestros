using Microsoft.EntityFrameworkCore;
using Siniestros.Domain.Accidents;

namespace Siniestros.Infrastructure.Persistence;

public sealed class SiniestrosDbContext : DbContext
{
    public SiniestrosDbContext(DbContextOptions<SiniestrosDbContext> options) : base(options) { }

    public DbSet<Accident> Accidents => Set<Accident>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accident>(b =>
        {
            b.ToTable("Accidents");

            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasConversion(
                    v => v.Value,
                    v => new AccidentId(v))
                .ValueGeneratedNever();

            b.Property(x => x.OccurredAt).IsRequired();
            b.Property(x => x.Department).HasMaxLength(100).IsRequired();
            b.Property(x => x.City).HasMaxLength(100).IsRequired();
            b.Property(x => x.Type).IsRequired();
            b.Property(x => x.VictimsCount).IsRequired();
            b.Property(x => x.Description).HasMaxLength(500);

            b.OwnsMany(x => x.Vehicles, vb =>
            {
                vb.ToTable("AccidentVehicles");
                vb.WithOwner().HasForeignKey("AccidentId");

                vb.Property<int>("Id");
                vb.HasKey("Id");

                vb.Property(x => x.Type).HasMaxLength(50).IsRequired();
                vb.Property(x => x.Plate).HasMaxLength(20);

                vb.HasIndex("AccidentId");
            });

            b.Navigation(x => x.Vehicles).Metadata.SetField("_vehicles");
            b.Navigation(x => x.Vehicles).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        base.OnModelCreating(modelBuilder);
    }
}
