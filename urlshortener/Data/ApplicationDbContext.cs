using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using urlshortener.Models;

namespace urlshortener.Data;

public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Models.Url> Urls { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Url>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Url>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Url>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(u => u.UserId);
            
        base.OnModelCreating(modelBuilder);
    }
}