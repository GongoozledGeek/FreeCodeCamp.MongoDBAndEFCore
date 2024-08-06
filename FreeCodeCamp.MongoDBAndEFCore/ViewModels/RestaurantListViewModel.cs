using FreeCodeCamp.MongoDBAndEFCore.Models;

namespace FreeCodeCamp.MongoDBAndEFCore.ViewModels;

public class RestaurantListViewModel
{
    public IEnumerable<Restaurant>? Restaurants { get; set; }
}