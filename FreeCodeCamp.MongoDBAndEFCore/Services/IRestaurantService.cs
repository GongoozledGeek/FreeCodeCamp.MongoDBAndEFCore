using FreeCodeCamp.MongoDBAndEFCore.Models;
using MongoDB.Bson;

namespace FreeCodeCamp.MongoDBAndEFCore.Services;

public interface IRestaurantService
{
    // All of these would be better as async
    IEnumerable<Restaurant> GetAllRestaurants();
    Restaurant? GetRestaurantById(ObjectId id);
    void AddRestaurant(Restaurant newRestaurant);
    void EditRestaurant(Restaurant updatedRestaurant);
    void DeleteRestaurant(Restaurant restaurantToDelete);
}