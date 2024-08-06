namespace FreeCodeCamp.MongoDBAndEFCore.Models.Configurations;

public record MongoDBSettings
{
    public string AtlasURI { get; init; }
    public string DatabaseName { get; init; }
}