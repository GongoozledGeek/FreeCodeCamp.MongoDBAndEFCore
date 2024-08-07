using FreeCodeCamp.MongoDBAndEFCore.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace FreeCodeCamp.MongoDBAndEFCore.Services;

public class RestaurantReservationDbContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; init; }
    
    public DbSet<Reservation> Reservations { get; init; }
    
    public RestaurantReservationDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Restaurant>(); // Linked to collection via Data Annotation in Model
        // modelBuilder.Entity<Reservation>(); // Original tutorial code
        modelBuilder.Entity<Reservation>().ToCollection("reservations");
    }
}