using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArtCollectionApi.Models
{
   public class PrintKind
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
      
        public string Name {get; set;} = null!;
    }   
}