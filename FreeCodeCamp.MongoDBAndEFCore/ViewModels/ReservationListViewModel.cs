using FreeCodeCamp.MongoDBAndEFCore.Models;

namespace FreeCodeCamp.MongoDBAndEFCore.ViewModels;

public class ReservationListViewModel
{
    public IEnumerable<Reservation>? Reservations { get; set; }
}