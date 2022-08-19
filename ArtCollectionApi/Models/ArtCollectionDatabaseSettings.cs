namespace ArtCollectionApi.Models
{
    public class ArtCollectionDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
        public string PrintsCollectionName {get; set;} = null!;
    }
}