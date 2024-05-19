using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Options;

namespace FoodModel;

public partial class FoodSourceContext : IdentityDbContext<FoodPlaceMenuItemsUser>
{
    public FoodSourceContext()
    {
    }

    public FoodSourceContext(DbContextOptions<FoodSourceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FoodPlace> FoodPlaces { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var config = builder.Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.MenuItemId).HasName("PK__MenuItem__8943F7227526083B");

            entity.HasOne(d => d.FoodPlace).WithMany(p => p.MenuItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuItem_FoodPlace");
        });

        modelBuilder.Entity<FoodPlace>(entity =>
        {
            entity.HasKey(e => e.FoodPlaceId).HasName("PK__FoodPlace");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
