using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FreeCodeCamp.MongoDBAndEFCore.Models;

// [Collection("reservations")] // Now linked in RestaurantReservationDbContext.OnModelCreating
public class Reservation
{
    public ObjectId Id { get; set; }

    public ObjectId RestaurantId { get; set; }

    public string? RestaurantName { get; set; }

    [Required(ErrorMessage = "The date and time is required to make this reservation")]
    [Display(Name = "Date")]
    public DateTime Date { get; set; }
}