using FreeCodeCamp.MongoDBAndEFCore.Models;
using FreeCodeCamp.MongoDBAndEFCore.Services;
using FreeCodeCamp.MongoDBAndEFCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FreeCodeCamp.MongoDBAndEFCore.Controllers;

public class RestaurantController : Controller
{
    private readonly IRestaurantService _RestaurantService;

    public RestaurantController(IRestaurantService RestaurantService)
    {
        _RestaurantService = RestaurantService;
    }
    public IActionResult Index()
    {
        RestaurantListViewModel viewModel = new()
        {
            Restaurants = _RestaurantService.GetAllRestaurants(),
        };
        return View(viewModel);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(RestaurantAddViewModel restaurantAddViewModel)
    {
        if (ModelState.IsValid)
        {
            Restaurant newRestaurant = new()
            {
                Name = restaurantAddViewModel.Restaurant.Name,
                Borough = restaurantAddViewModel.Restaurant.Borough,
                Cuisine = restaurantAddViewModel.Restaurant.Cuisine
            };

            _RestaurantService.AddRestaurant(newRestaurant);
            return RedirectToAction("Index");
        }

        return View(restaurantAddViewModel);
    }

    public IActionResult Edit(ObjectId id)
    {
        if (id == null || id == ObjectId.Empty)
        {
            return NotFound();
        }

        var selectedRestaurant = _RestaurantService.GetRestaurantById(id);
        return View(selectedRestaurant);
    }

    [HttpPost]
    public IActionResult Edit(Restaurant restaurant)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _RestaurantService.EditRestaurant(restaurant);
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Updating the restaurant failed, please try again! Error: {ex.Message}");
        }

        return View(restaurant);
    }

    public IActionResult Delete(ObjectId id)
    {
        if (id == null || id == ObjectId.Empty)
        {
            return NotFound();
        }

        var selectedRestaurant = _RestaurantService.GetRestaurantById(id);
        return View(selectedRestaurant);
    }

    [HttpPost]
    public IActionResult Delete(Restaurant restaurant)
    {
        if (restaurant.Id == ObjectId.Empty)
        {
            ViewData["ErrorMessage"] = "Deleting the restaurant failed, invalid ID!";
            return View();
        }

        try
        {
            _RestaurantService.DeleteRestaurant(restaurant);
            TempData["RestaurantDeleted"] = "Restaurant deleted successfully!";

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = $"Deleting the restaurant failed, please try again! Error: {ex.Message}";
        }

        var selectedRestaurant = _RestaurantService.GetRestaurantById(restaurant.Id);
        return View(selectedRestaurant);
    }
}