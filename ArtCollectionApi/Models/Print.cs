using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArtCollectionApi.Models
{
   public class Print
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Title {get; set;} = null!;
        public string EstimatedDate {get; set;} = null!;
        public string Source {get; set;} = null!;
        public string References {get; set;} = null!;

        public string Notes {get; set;} = null!;

        public string[] Labels{get ; set;} = null!;

        public string ArtistName {get; set;} = null!;

        public float HPGP {get; set;}
        public string Size {get; set;} = null!;

        public string ImageSize {get; set;} = null!;
    }   
}