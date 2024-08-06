using FreeCodeCamp.MongoDBAndEFCore.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace FreeCodeCamp.MongoDBAndEFCore.Services;

public class ReservationService : IReservationService
{
    private readonly RestaurantReservationDbContext _restaurantDbContext;

    public ReservationService(RestaurantReservationDbContext restaurantDbContext)
    {
        _restaurantDbContext = restaurantDbContext;
    }

    public void AddReservation(Reservation newReservation)
    {
        var bookedRestaurant =
            _restaurantDbContext.Restaurants.FirstOrDefault(c => c.Id == newReservation.RestaurantId);

        if(bookedRestaurant == null)
        {
            throw new ArgumentException("The restaurant to be reserved cannot be found.");
        }

        newReservation.RestaurantName = bookedRestaurant.Name;

        _restaurantDbContext.Reservations.Add(newReservation);

        _restaurantDbContext.ChangeTracker.DetectChanges();
        Console.WriteLine(_restaurantDbContext.ChangeTracker.DebugView.LongView);

        _restaurantDbContext.SaveChanges();
    }

    public void DeleteReservation(Reservation reservation)
    {
        var reservationToDelete = _restaurantDbContext.Reservations.FirstOrDefault(b => b.Id == reservation.Id);

        if(reservationToDelete != null)
        {
            _restaurantDbContext.Reservations.Remove(reservationToDelete);

            _restaurantDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_restaurantDbContext.ChangeTracker.DebugView.LongView);

            _restaurantDbContext.SaveChanges();
        }
        else
        {
            throw new ArgumentException("The reservation to delete cannot be found.");
        }
    }

    public void EditReservation(Reservation updatedReservation)
    {
        var reservationToUpdate = _restaurantDbContext.Reservations.FirstOrDefault(b => b.Id == updatedReservation.Id);

        if(reservationToUpdate != null)
        {
            reservationToUpdate.Date = updatedReservation.Date;

            _restaurantDbContext.Reservations.Update(reservationToUpdate);

            _restaurantDbContext.ChangeTracker.DetectChanges();
            _restaurantDbContext.SaveChanges();

            Console.WriteLine(_restaurantDbContext.ChangeTracker.DebugView.LongView);
        }
        else
        {
            throw new ArgumentException("Reservation to be updated cannot be found");
        }
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _restaurantDbContext.Reservations.OrderBy(b => b.Date).Take(20).AsNoTracking()
                                   .AsEnumerable<Reservation>();
    }

    public Reservation? GetReservationById(ObjectId id)
    {
        return _restaurantDbContext.Reservations.AsNoTracking().FirstOrDefault(b => b.Id == id);
    }
}