using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FreeCodeCamp.MongoDBAndEFCore.Models;

[Collection("restaurants")]
public class Restaurant
{
    public ObjectId Id { get; set; }

    [Required(ErrorMessage = "You must provide a name")]
    // [Display(Name = "Name")] // Original code used Display attributes as the properties where in Camel Case
    public string? Name { get; set; }

    [Required(ErrorMessage = "You must add a cuisine type")]
    [Display(Name = "Cuisine Type")]
    public string? Cuisine { get; set; }

    [Required(ErrorMessage = "You must add the borough of the restaurant")]
    public string? Borough { get; set; }
}